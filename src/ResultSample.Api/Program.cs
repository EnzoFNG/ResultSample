using ResultSample.Abstractions.IoC;
using ResultSample.Api.Configurations;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        });
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddSwaggerConfiguration()
    .AddDatabaseConfiguration(builder.Configuration)
    .RegisterInternalServices();

var app = builder.Build();

if (!app.Environment.IsProduction())
{
    app.UseSwaggerSetup();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();