using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MusicDb;

public partial class MusicContext : DbContext
{
    public MusicContext(DbContextOptions<MusicContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<Record> Records { get; set; }

    public virtual DbSet<RecordType> RecordTypes { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Record>(entity =>
        {
            entity.HasIndex(e => e.ArtistId, "IX_Records_ArtistId");

            entity.HasIndex(e => e.RecordTypeId, "IX_Records_RecordTypeId");

            entity.HasOne(d => d.Artist).WithMany(p => p.Records).HasForeignKey(d => d.ArtistId);

            entity.HasOne(d => d.RecordType).WithMany(p => p.Records).HasForeignKey(d => d.RecordTypeId);
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.HasIndex(e => e.RecordId, "IX_Songs_RecordId");

            entity.HasOne(d => d.Record).WithMany(p => p.Songs).HasForeignKey(d => d.RecordId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
