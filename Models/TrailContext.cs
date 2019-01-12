using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class TrailContext : IdentityDbContext<User>
{
    public TrailContext(DbContextOptions<TrailContext> options)
        : base(options)
    { }

    public DbSet<Trail> Trails { get; set; }
    public DbSet<TrailEdit> TrailEdits { get; set; }
    public DbSet<Image> Images { get; set; }
}