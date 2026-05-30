using Microsoft.OpenApi.Models;

namespace Bansi.Api.Configurations
{
    public static class ConfigurationSwagger
    {
        public static IServiceCollection AddConfigurationSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Api Bansi",
                        Version = "0.0.0.1",
                        Description = "Servicios Rest Examen",
                    });
            });
            return services;
        }
        public static IApplicationBuilder UseConfigurationSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.InjectStylesheet("/css/main.css");
                    c.InjectJavascript("/js/swagger.js");
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1");
                    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
                });

                app.UseReDoc(c =>
                {
                    c.DocumentTitle = "Api Bansi";
                    c.SpecUrl = "../swagger/v1/swagger.json";
                });
            }
            else
            {
                app.UseReDoc(c =>
                {
                    c.DocumentTitle = "Api Bansi";
                    c.SpecUrl = "../swagger/v1/swagger.json";
                });
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("../swagger/v1/swagger.json", "Api v1");
                });
            }
            return app;
        }

    }
    
}
