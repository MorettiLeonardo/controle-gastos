using ControleGastos.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ControleGastos.Ioc.Extensions
{
    internal static class DbContextExtension
    {
        internal static IServiceCollection RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionStr = configuration.GetConnectionString("DefaultConnection")!;

            services.AddDbContext<ApplicationDbContext>(opts =>
            {
                opts.UseNpgsql(connectionStr,
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)).LogTo(Console.WriteLine, LogLevel.Information);
            });

            return services;
        }
    }
}
