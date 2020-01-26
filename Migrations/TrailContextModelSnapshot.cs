﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace trails.Migrations
{
    [DbContext(typeof(TrailContext))]
    partial class TrailContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FavouriteTrails", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("TrailId");

                    b.HasKey("UserId", "TrailId");

                    b.HasIndex("TrailId");

                    b.ToTable("FavouriteTrails");
                });

            modelBuilder.Entity("Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Base64Preview");

                    b.Property<int>("EditId");

                    b.Property<string>("Name");

                    b.Property<string>("ThumbnailUrl");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("EditId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedName");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("ProviderKey");

                    b.HasKey("UserId");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("UserId");

                    b.HasKey("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<string>("Value")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<int>("UserId");

                    b.HasKey("Value");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("Trail", b =>
                {
                    b.Property<int>("TrailId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Approved");

                    b.Property<string>("Description");

                    b.Property<double>("Distance");

                    b.Property<int?>("EditId");

                    b.Property<int>("Elevation");

                    b.Property<string>("Location");

                    b.Property<double>("MaxDuration");

                    b.Property<string>("MaxSeason");

                    b.Property<double>("MinDuration");

                    b.Property<string>("MinSeason");

                    b.Property<double>("Rating");

                    b.Property<string>("Title");

                    b.HasKey("TrailId");

                    b.HasIndex("EditId");

                    b.ToTable("Trails");
                });

            modelBuilder.Entity("TrailEdit", b =>
                {
                    b.Property<int>("EditId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<double>("Distance");

                    b.Property<int>("Elevation");

                    b.Property<string>("Location");

                    b.Property<double>("MaxDuration");

                    b.Property<string>("MaxSeason");

                    b.Property<double>("MinDuration");

                    b.Property<string>("MinSeason");

                    b.Property<double>("Rating");

                    b.Property<string>("Title");

                    b.Property<int>("TrailId");

                    b.HasKey("EditId");

                    b.ToTable("TrailEdits");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FavouriteTrails", b =>
                {
                    b.HasOne("Trail", "Trail")
                        .WithMany("FavouriteTrails")
                        .HasForeignKey("TrailId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("User", "User")
                        .WithMany("FavouriteTrails")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Image", b =>
                {
                    b.HasOne("TrailEdit")
                        .WithMany("Images")
                        .HasForeignKey("EditId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Trail", b =>
                {
                    b.HasOne("TrailEdit", "Edit")
                        .WithMany()
                        .HasForeignKey("EditId");
                });
#pragma warning restore 612, 618
        }
    }
}
