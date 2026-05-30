using Bansi.Domain.Interfaces;
using Bansi.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Bansi.Api.Configurations
{
    public static class ConfigurationRepositories
    {
        public static IServiceCollection AddConfigurationRepositories(this IServiceCollection services)
        {
            services.AddKeyedScoped<IExamenRepository, ExamenEfRepository>("EF");
            services.AddKeyedScoped<IExamenRepository, ExamenSpRepository>("SP");
            return services;
        }
    }
}
