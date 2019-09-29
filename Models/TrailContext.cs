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
}