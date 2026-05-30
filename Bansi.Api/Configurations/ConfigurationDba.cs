using Bansi.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Bansi.Api.Configurations
{
    public static class ConfigurationDba
    {
        public static IServiceCollection AddConfigurationDba(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<BansiContext>(c =>
                c.UseSqlServer(
                    Configuration.GetConnectionString("BansiDB"),
                    b => b.MigrationsAssembly("Bansi.Infrastructure")
                ));

            return services;
        }
    }
}
