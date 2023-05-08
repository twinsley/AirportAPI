using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AirportAPI.Entities;

public partial class Wx1Context : DbContext
{
    public Wx1Context()
    {
    }

    public Wx1Context(DbContextOptions<Wx1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Wxdatum> Wxdata { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Wxdatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("wxdata");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("date");
            entity.Property(e => e.Humidity).HasColumnName("humidity");
            entity.Property(e => e.Identifier)
                .HasColumnType("text")
                .HasColumnName("identifier");
            entity.Property(e => e.Temperature).HasColumnName("temperature");
            entity.Property(e => e.Winddirection).HasColumnName("winddirection");
            entity.Property(e => e.Windspeed).HasColumnName("windspeed");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
