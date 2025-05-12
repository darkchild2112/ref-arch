using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Ref.Arch.Api.Clients;
using Ref.Arch.Api.Endpoints.Todos;
using Ref.Arch.Api.Endpoints.Todos.Get;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();

// Handlers
builder.Services.AddScoped<GetTodosHandler>();

// Clients
builder.Services.AddJsonPlaceholderClient(builder.Configuration);



var app = builder.Build();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    },
    AllowCachingResponses = false
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Open API V1");
    });
}

app
    .MapGroup("/api/v1/todos")
    .WithTags("Todos")
    .MapTodosEndpoints();

app.UseHttpsRedirection();

app.Run();

// Required to expose for testing
public partial class Program { }
