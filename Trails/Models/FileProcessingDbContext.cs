using System;
using Microsoft.EntityFrameworkCore;
using Trails.FileProcessing.Models;

namespace Trails.Models
{
  public class FileProcessingDbContext : DbContext
  {
      public FileProcessingDbContext(DbContextOptions<FileProcessingDbContext> options)
          : base(options)
      { }

      public DbSet<FileTransform> FileTransforms { get; set; }
      public DbSet<TransformJob> Transforms { get; set; }

      public DbSet<AppliedFileTransforms> AppliedFileTransforms { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
        modelBuilder.Entity<FileTransform>(b => {
          b.HasKey(x => x.id);
          b.Property(x => x.status)
            .HasConversion(x => x.ToString(), // to converter
                x => (FileStatus) Enum.Parse(typeof(FileStatus), x));
        });
            
        modelBuilder.Entity<TransformJob>(b =>
        {
            b.HasKey(i => i.id);
            b.Property(x => x.status)
            .HasConversion(x => x.ToString(), // to converter
                x => (FileStatus) Enum.Parse(typeof(FileStatus), x));
        });

        modelBuilder.Entity<AppliedFileTransforms>(b => b.HasKey(v => new { v.fileId, v.transformJobId }));

        modelBuilder.Entity<AppliedFileTransforms>()
            .Property(e => e.appliedTransforms)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
      }
  }
}
