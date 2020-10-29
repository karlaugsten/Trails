using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class TrailContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public TrailContext(DbContextOptions<TrailContext> options)
        : base(options)
    { }

    public DbSet<Trail> Trails { get; set; }
    public DbSet<TrailEdit> TrailEdits { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Map> Maps { get; set; }

    public DbSet<TrailEditImage> TrailEditImages { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserToken<int>>()
                .HasKey(x => x.Value);
            modelBuilder.Entity<IdentityUserRole<int>>(b =>
            {
                b.HasKey(i => new {i.UserId, i.RoleId});
            });

            modelBuilder.Entity<TrailEditImage>(b =>
            {
                b.HasKey(i => new {i.ImageId, i.EditId});
            });

            modelBuilder.Entity<TrailEditImage>()
                .HasOne(x => x.Edit)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.EditId);
            modelBuilder.Entity<IdentityUserLogin<int>>()
                .HasKey(x => x.UserId);
            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Trail>()
                .HasKey(x => x.TrailId);
            modelBuilder.Entity<FavouriteTrails>()
                .HasKey(x => new { x.UserId, x.TrailId });
            modelBuilder.Entity<FavouriteTrails>()
                .HasOne(x => x.User)
                .WithMany(m => m.FavouriteTrails)
                .HasForeignKey(x => x.UserId);
            modelBuilder.Entity<FavouriteTrails>()
                .HasOne(x => x.Trail)
                .WithMany(e => e.FavouriteTrails)
                .HasForeignKey(x => x.TrailId);

            modelBuilder.Entity<FavouriteTrails>()
                .HasOne(x => x.Trail)
                .WithMany(e => e.FavouriteTrails)
                .HasForeignKey(x => x.TrailId);
        }
}