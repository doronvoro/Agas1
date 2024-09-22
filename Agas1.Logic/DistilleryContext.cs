﻿using Microsoft.EntityFrameworkCore;
using Agas1.Logic.Models;

namespace Agas1.Logic
{
    public class DistilleryContext : DbContext
    {
        public DistilleryContext(DbContextOptions<DistilleryContext> options) : base(options)
        {
        }

        public DbSet<Tank> Tanks { get; set; }
        public DbSet<LiquidType> LiquidTypes { get; set; }
        public DbSet<LiquidAddition> LiquidAdditions { get; set; }
        public DbSet<TankLog> TankLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed LiquidTypes
            modelBuilder.Entity<LiquidType>().HasData(
                new LiquidType { Id = 1, Name = "Water" },
                new LiquidType { Id = 2, Name = "Alcohol" }
            );
        }
    }
}