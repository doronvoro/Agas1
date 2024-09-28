using Agas1.Logic.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Agas1.Logic
{ 
public class DistilleryContext : IdentityDbContext
{
    public DistilleryContext(DbContextOptions<DistilleryContext> options) : base(options)
    {
    }

    public DbSet<Tank> Tanks { get; set; }
    public DbSet<LiquidType> LiquidTypes { get; set; }
   // public DbSet<LiquidAddition> LiquidAdditions { get; set; }
    public DbSet<TankLog> TankLogs { get; set; }

    public DbSet<Material> Materials { get; set; }
    public DbSet<TankProcess> TankProcesses { get; set; }  // Updated name
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LiquidType>().HasData(
           new LiquidType { Id = 1, Name = "Water" },
           new LiquidType { Id = 2, Name = "Alcohol" }
       );

        modelBuilder.Entity<Material>().HasData(
          new Material { Id = 1, Name = "Water" },
          new Material { Id = 2, Name = "Alcohol" },
          new Material { Id = 3, Name = "Spice" }
      );

        // Seed default tank processes
        modelBuilder.Entity<TankProcess>().HasData(  // Updated seeding for TankProcess
            new TankProcess { Id = 1, Name = "Distillation" },
            new TankProcess { Id = 2, Name = "Fermentation" },
            new TankProcess { Id = 3, Name = "Soaking" }
        );

        base.OnModelCreating(modelBuilder);  // Ensure Identity-related tables are created
    }

}
}
