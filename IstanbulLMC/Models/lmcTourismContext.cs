using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IstanbulLMC.Models;

public partial class lmcTourismContext : IdentityDbContext<AppUser>
{
    public lmcTourismContext(DbContextOptions<lmcTourismContext> options) : base(options)
    {
    }



    public virtual DbSet<Transfer> Transfer { get; set; }
    public virtual DbSet<VehicleCategory> VehicleCategory { get; set; }
    public virtual DbSet<TransferService> TransferService { get; set; }
    public virtual DbSet<Service> Service { get; set; }
    public virtual DbSet<Currency> Currency { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Transfer>().ToTable("Transfer", schema: "dbo");
        modelBuilder.Entity<VehicleCategory>().ToTable("VehicleCategory", schema: "dbo");
        modelBuilder.Entity<TransferService>().ToTable("TransferServices", schema: "dbo");
        modelBuilder.Entity<Service>().ToTable("Service", schema: "dbo");
        modelBuilder.Entity<Currency>().ToTable("Currency", schema: "dbo");
    }

}
