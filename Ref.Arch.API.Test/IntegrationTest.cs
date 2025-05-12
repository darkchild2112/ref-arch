using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Text.Json;
using WireMock.Server;

namespace Ref.Arch.Api.Test;

public abstract class IntegrationTest : IDisposable
{
    private bool _disposed = false;
    protected static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    protected WireMockServer JsonPlaceholderApi { get; init; }

    protected HttpClient Client { get; }

    public IntegrationTest()
    {
        JsonPlaceholderApi = WireMockServer.Start();
        Client = CreateClient();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected HttpClient CreateClient() =>
        new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder
                .ConfigureAppConfiguration(ConfigureAppConfig)
                .ConfigureTestServices(SetupTestServices)
            )
            .CreateClient();

    protected void ConfigureAppConfig(IConfigurationBuilder builder)
    {
        builder.AddInMemoryCollection(GetConfigurationsToOverride());
        builder.AddJsonFile(AppConfigPath, false);
    }

    protected virtual Dictionary<string, string?> GetConfigurationsToOverride() =>
        new()
        {
            { "JsonPlaceholder:Url", JsonPlaceholderApi.Url },
        };

    protected void SetupTestServices(IServiceCollection services) { }

    private static string AppConfigPath => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "appsettings.json");

    private void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            Client.Dispose();
            JsonPlaceholderApi.Dispose();
        }

        _disposed = true;
    }
}
