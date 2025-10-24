using System;
using System.Collections.Generic;
using M223PunchclockDotnet.Model;
using Microsoft.EntityFrameworkCore;

namespace M223PunchclockDotnet.Model;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Entry> Entries { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres;Ssl Mode=Disable;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entry>().ToTable("Entry");
        modelBuilder.Entity<Category>()
            .ToTable("Category")
            .HasMany(category => category.Entries)
            .WithOne(entry => entry.Category)
            .IsRequired(true);
        modelBuilder.Entity<Tag>()
            .ToTable("Tag")
            .HasMany(tag => tag.Entries)
            .WithMany(entry => entry.Tags);


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
