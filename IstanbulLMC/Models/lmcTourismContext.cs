using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IstanbulLMC.Models;

public partial class lmcTourismContext : DbContext
{
    public lmcTourismContext()
    {
    }

    public lmcTourismContext(DbContextOptions<lmcTourismContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Passenger> Passenger { get; set; }

    public virtual DbSet<Transfer> Transfer { get; set; }

    public virtual DbSet<VehicleCategory> VehicleCategory { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        //Scaffold-DbContext "Data Source=104.247.162.242\MSSQLSERVER2019;Initial Catalog=kadirocs_lmc_Tourism;Persist Security Info=True;TrustServerCertificate=True;User ID=kadirocs_lmc_Tourism;Password=lmc_Tourism?2023" Microsoft.EntityFrameworkCore.SqlServer -f -o Models -context "lmcTourismContext" -UseDatabaseNames -NoPluralize

        => optionsBuilder.UseSqlServer("Data Source=104.247.162.242\\MSSQLSERVER2019;Initial Catalog=kadirocs_lmc_Tourism;Persist Security Info=True;TrustServerCertificate=True;User ID=kadirocs_lmc_Tourism;Password=lmc_Tourism?2023");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("kadirocs_lmc_Tourism");

        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_Passenger_ID");

            entity.ToTable("Passenger", "dbo");

            entity.Property(e => e.InsertID)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateID)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Transfer).WithMany(p => p.Passenger)
                .HasForeignKey(d => d.TransferID)
                .HasConstraintName("FK_Passenger_Transfer_ID");
        });

        modelBuilder.Entity<Transfer>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_Transfer_ID");

            entity.ToTable("Transfer", "dbo");

            entity.Property(e => e.Distance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FromPlaceID)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.InsertID)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.KMPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ToPlaceID)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdateID)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.VehicleCategory).WithMany(p => p.Transfer)
                .HasForeignKey(d => d.VehicleCategoryID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transfer_VehicleCategory_ID");
        });

        modelBuilder.Entity<VehicleCategory>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_VehicleCategory_ID");

            entity.ToTable("VehicleCategory", "dbo");

            entity.Property(e => e.InsertID)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.KMPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MaxDistance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SeateCount)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateID)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
