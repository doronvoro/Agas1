using Agas1.Logic.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Agas1.Logic
{
    public static class LogicStartup
    {
        public static void AddAgas1Logic(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<DistilleryContext>(options =>
    options.UseSqlite("Data Source=distillery.db" , b => b.MigrationsAssembly("Agas1.Logic")));

         //   Change your migrations assembly by using DbContextOptionsBuilder.E.g.options.UseSqlServer(connection, b => b.MigrationsAssembly("Agas1.App")). By default, the migrations assembly is the assembly containing the DbContext.


            //serviceCollection.AddDbContext<DistilleryContext>(options =>
            //    options.UseSqlite("Data Source=distillery.db"));  // Ensure DbContext is configured with SQLite
            serviceCollection.AddScoped<DistilleryService>();
            serviceCollection.AddScoped<LiquidTypeService>();
        }
    }
}
