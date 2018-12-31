using Microsoft.EntityFrameworkCore;

public class TrailContext : DbContext
{
    public TrailContext(DbContextOptions<TrailContext> options)
        : base(options)
    { }

    public DbSet<Trail> Trails { get; set; }
    public DbSet<TrailEdit> TrailEdits { get; set; }
    public DbSet<Image> Images { get; set; }
}