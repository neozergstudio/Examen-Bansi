using Bansi.Application.Examenes;
using Bansi.Application.Examenes.DTOs;
using Bansi.Application.Examenes.Services;
using Bansi.Application.Examenes.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Bansi.Api.Configurations
{
    public static class ConfigurationServices
    {
        public static IServiceCollection AddConfigurationServices(this IServiceCollection services)
        {
            services.AddScoped<IValidator<ExamenDto>, ExamenValidator>();
            services.AddScoped<IValidator<ExamenInputDto>, ExamenInputValidator>();
            services.AddScoped<IExamenService, clsExamen>();
            return services;
        }
    }
}
