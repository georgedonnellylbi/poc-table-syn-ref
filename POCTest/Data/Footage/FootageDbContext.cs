using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using POCTest.Models.Footage;

namespace POCTest.Data.Footage;

public partial class FootageDbContext : DbContext
{
    public FootageDbContext(DbContextOptions<FootageDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Film> Films { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Film>(entity =>
        {
            entity.HasKey(e => e.FilmId).HasName("PK__Film__6D1D217C35004CD8");

            entity.ToTable("Film");

            entity.Property(e => e.Title).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
