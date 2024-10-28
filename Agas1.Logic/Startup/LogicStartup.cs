using Agas1.Logic.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Agas1.Logic
{
    public static class LogicStartup
    {
        public static void AddAgas1Logic(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<DistilleryContext>(options =>
    options.UseSqlite("Data Source=distillery.db", b => b.MigrationsAssembly("Agas1.Logic")));

            serviceCollection.AddScoped<DistilleryService>();
            serviceCollection.AddScoped<LiquidTypeService>();
        }
    }
}
