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

    public virtual DbSet<Formdatum> Formdata { get; set; }

    public virtual DbSet<Wxdatum> Wxdata { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Formdatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("formdata");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Airport)
                .HasMaxLength(500)
                .HasColumnName("airport");
            entity.Property(e => e.Comments)
                .HasMaxLength(10000)
                .HasColumnName("comments");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("lastName");
        });

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
