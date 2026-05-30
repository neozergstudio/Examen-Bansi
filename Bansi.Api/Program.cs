using Bansi.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfigurationDba(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddConfigurationRepositories();
builder.Services.AddConfigurationServices();
builder.Services.AddConfigurationSwagger();
var app = builder.Build();

app.UseConfigurationSwagger(app.Environment);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
