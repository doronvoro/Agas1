using Agas1.Logic.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agas1.Logic
{
    public static class LogicStartup
    {
        public static void AddAgas1Logic(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<DistilleryContext>(options =>
    options.UseSqlite("Data Source=distillery.db" ));



            //serviceCollection.AddDbContext<DistilleryContext>(options =>
            //    options.UseSqlite("Data Source=distillery.db"));  // Ensure DbContext is configured with SQLite
            serviceCollection.AddScoped<DistilleryService>();
            serviceCollection.AddScoped<LiquidTypeService>();
        }
    }
}
