using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Trails.Encryption;
using Trails.Authentication;
using Trails.Repositories;
using Amazon.S3;
using Microsoft.Extensions.Logging;
using Trails.FileProcessing;
using Trails.Transforms;
using Trails.Models;
using Elastic.Apm.NetCoreAll;
using Serilog;

namespace Trails
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        private IHostingEnvironment CurrentEnvironment{ get; set; } 

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
            .AddMvc(options => options.EnableEndpointRouting = false)
            .SetCompatibilityVersion(CompatibilityVersion.Latest)
            .AddJsonOptions(jo =>
            {
                jo.JsonSerializerOptions.IgnoreNullValues = true;
            });

            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonS3>();
            if(CurrentEnvironment.IsDevelopment()) {
                // In development, use the local file repository
                // var imageRepo = new LocalFileRepository(Configuration["ImageRepoFolder"]);
                //services.AddTransient<IFileRepository>((serviceProvider) => new LocalFileRepository(Configuration["ImageRepoFolder"]));
                /*services.AddTransient<IImageRepository>((provider) => 
                    new FileImageRepository(
                        new LocalFileRepository(Configuration["ImageRepoFolder"]),
                        provider.GetService<TrailContext>(),
                        Configuration,
                        provider.GetService<IImageProcessor>()
                    ));*/
                services.AddTransient<IFileRepository>(sp => new LocalFileRepository(Configuration["ImageRepoFolder"]));
                services.AddTransient<IImageRepository, ProcessedImageRepository>();
                services.AddTransient<IGpxRepository>(provider =>
                    new GpxRepository(provider.GetService<TrailContext>(),
                        provider.GetService<FileProcessor>(), 
                        provider.GetService<ILoggerFactory>(),
                        new LocalFileRepository(Configuration["GpxRepoFolder"]))
                );
            } else {
                // In prod, use the S3 file repository
                services.AddTransient<IFileRepository>(provider => new S3FileRepository(provider.GetService<ILoggerFactory>(),
                            Configuration["S3ImageBucket"],
                            provider.GetService<IAmazonS3>()));
                services.AddTransient<IImageRepository, ProcessedImageRepository>();

                services.AddTransient<IGpxRepository>(provider =>
                    new GpxRepository(provider.GetService<TrailContext>(),
                        provider.GetService<FileProcessor>(), 
                        provider.GetService<ILoggerFactory>(),
                        new S3FileRepository(provider.GetService<ILoggerFactory>(),
                            Configuration["S3GPXBucket"],
                            provider.GetService<IAmazonS3>())
                        )
                );
            }  

            // For now use the test database.
            services.AddDbContext<TrailContext>(options => { 
                if(CurrentEnvironment.IsDevelopment()) {
                    options.UseMySql(Configuration["DatabaseConnectionString"]);

                    //options.UseSqlite("Data Source=trails.db");
                } else {
                    options.UseMySql(Configuration["DatabaseConnectionString"]);
                }  
            });

            services.AddDbContext<FileProcessingDbContext>(options => { 
                if(CurrentEnvironment.IsDevelopment()) {
                    options.UseMySql(Configuration["DatabaseConnectionString"]);

                    //options.UseSqlite("Data Source=trails.db");
                } else {
                    options.UseMySql(Configuration["DatabaseConnectionString"]);
                }  
            });
            
            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "trails/build";
            });

            services.AddIdentity<User, IdentityRole<int>>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
                config.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<TrailContext>()
            .AddDefaultTokenProviders();

            /*services.AddDefaultIdentity<User>()
            .AddDefaultTokenProviders()
            .AddRoles<IdentityRole<int>>()
            .AddEntityFrameworkStores<TrailContext>();*/

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false; 
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequiredLength = 6;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = System.TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            ISigningKeyResolver signingKey = new AppSettingsSymetricKeyResolver(Configuration, "SigningKey"); // TODO: Add signing key to the appsettings.
            services.AddSingleton<ISigningKeyResolver>(signingKey);
            services.AddScoped<ITokenFactory, JwtTokenFactory>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey.GetSecurityKey(),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = System.TimeSpan.FromMinutes(5),
                    RequireSignedTokens = true,
                    // Ensure the token hasn't expired:
                    RequireExpirationTime = true,
                    ValidateLifetime = true
                };
            });

            // Add all the authorization claims policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanEdit", policy => policy.RequireClaim("CanEditTrails", new[] { "true" }));
                options.AddPolicy("CanCommit", policy => policy.RequireClaim("CanCommitTrails", new[] { "true" }));
            });

            ConfigureFileProcessing(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseAllElasticApm(Configuration);
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "trails";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedRoles(RoleManager<IdentityRole<int>> roleManager)
        {
            if (!roleManager.RoleExistsAsync
        ("NormalUser").Result)
            {
                IdentityRole<int> role = new IdentityRole<int>();
                role.Name = "NormalUser";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync
        ("Administrator").Result)
            {
                IdentityRole<int> role = new IdentityRole<int>();
                role.Name = "Administrator";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                var realRole = roleManager.FindByNameAsync("Administrator").Result;
                var one = roleManager.AddClaimAsync(realRole, new Claim("CanEditTrails", "true")).Result;
                var two = roleManager.AddClaimAsync(realRole, new Claim("CanCommitTrails", "true")).Result;
            } else {
                var realRole = roleManager.FindByNameAsync("Administrator").Result;
                var one = roleManager.AddClaimAsync(realRole, new Claim("CanEditTrails", "true")).Result;
                var two = roleManager.AddClaimAsync(realRole, new Claim("CanCommitTrails", "true")).Result;
            }
        }

        public static void SeedUsers(UserManager<User> userManager)
        {
            var karl = userManager.FindByNameAsync
        ("karl").Result;
            if (karl == null)
            {
                User user = new User();
                user.UserName = "karl";
                user.Email = "karlaugsten@gmail.com";
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync
                (user, "test123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "Administrator").Wait();
                }
            } else {
                karl.EmailConfirmed = true;
                userManager.UpdateAsync(karl).Wait();
                userManager.AddToRoleAsync(karl,
                                        "Administrator").Wait();
            }
        }

        public void ConfigureFileProcessing(IServiceCollection services) {
            // Add all the transforms.
            services.AddScoped<S3ImageStreamTransform>();
            services.AddScoped<SaveBlurBase64EndTransform>();
            services.AddScoped<SaveJpgImageToS3Transform>();
            services.AddScoped<SaveMainImageEndTransform>();
            services.AddScoped<SaveThumbnailImageEndTransform>();
            services.AddScoped<StreamToBase64>();
            services.AddScoped<StreamToImageTransform>();
            services.AddScoped<ImagePreserveAspectResizeTransform>();
            services.AddScoped<ImageResizeAndCropTransform40x25>();
            services.AddScoped<ImageResizeAndCropTransform800x500>();
            services.AddScoped<ImageToJpegMemStreamTransform94>();
            services.AddScoped<ImageToJpegMemStreamTransform90>();
            services.AddScoped<ImageToJpegMemStreamTransform20>();

            // Map transforms
            services.AddScoped<S3MapStreamTransform>(sp => {
                if(CurrentEnvironment.IsDevelopment()) {
                    return new S3MapStreamTransform(new LocalFileRepository(Configuration["GpxRepoFolder"]));
                } else {
                    return new S3MapStreamTransform(new S3FileRepository(sp.GetService<ILoggerFactory>(),
                            Configuration["S3GPXBucket"],
                            sp.GetService<IAmazonS3>())
                        );
                }
            });
            services.AddScoped<ElevationPolylineConverter>();
            services.AddScoped<ElevationPolylinePopulator>();
            services.AddScoped<LocationInterpolator50MeterStep>();
            services.AddScoped<LocationPolylineConverter>();
            services.AddScoped<LocationPolylinePopulator>();
            services.AddScoped<StartEndLocationPopulator>();
            services.AddScoped<StreamToXmlDocument>();
            services.AddScoped<XmlDocumentElevation20MeterStep>();
            services.AddScoped<XmlDocumentToLocations>();
            
            services.AddSingleton<ITransformJobQueue, InMemoryTransformJobQueue>();
            services.AddFileProcessing(transforms => {
                TrailsTransforms.loadTrailImageTransforms(services.BuildServiceProvider(), transforms);
                TrailsTransforms.loadGpxFileTransforms(services.BuildServiceProvider(), transforms);
            });
            services.AddScoped<IFileProcessingRepository, FileProcessingRepository>();
        }
    }
}
