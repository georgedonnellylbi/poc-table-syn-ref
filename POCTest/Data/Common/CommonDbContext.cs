using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using POCTest.Models.Common;

namespace POCTest.Data.Common;

public partial class CommonDbContext : DbContext
{
    public CommonDbContext(DbContextOptions<CommonDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Club> Clubs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Club>(entity =>
        {
            entity.HasKey(e => e.ClubId).HasName("PK__Clubs__D35058E7BB499C4F");

            entity.HasIndex(e => e.CanonicalName, "IX_Clubs_CanonicalName");

            entity.HasIndex(e => e.CanonicalName, "UQ__Clubs__BF902B58A84443A0").IsUnique();

            entity.Property(e => e.CanonicalName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
