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

    public virtual DbSet<TagEntry> TagEntries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres;Ssl Mode=Disable;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("category_pkey");

            entity.ToTable("category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Entry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("entry_pkey");

            entity.ToTable("entry");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CheckIn).HasColumnType("timestamp without time zone");
            entity.Property(e => e.CheckOut).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Category).WithMany(p => p.Entries)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_entry_category");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tag_pkey");

            entity.ToTable("tag");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
        });

        modelBuilder.Entity<TagEntry>(entity =>
        {
            entity.HasKey(e => e.TagEntryId).HasName("tag_entry_pkey");

            entity.ToTable("tag_entry");

            entity.Property(e => e.TagEntryId).HasColumnName("tag_entry_id");
            entity.Property(e => e.EntryId).HasColumnName("entry_id");
            entity.Property(e => e.TagId).HasColumnName("tag_id");

            entity.HasOne(d => d.Entry).WithMany(p => p.TagEntries)
                .HasForeignKey(d => d.EntryId)
                .HasConstraintName("tag_entry_entry_id_fkey");

            entity.HasOne(d => d.Tag).WithMany(p => p.TagEntries)
                .HasForeignKey(d => d.TagId)
                .HasConstraintName("tag_entry_tag_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
