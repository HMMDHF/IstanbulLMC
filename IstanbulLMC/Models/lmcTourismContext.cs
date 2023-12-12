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





    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        base.OnModelCreating(modelBuilder);


    }

}
