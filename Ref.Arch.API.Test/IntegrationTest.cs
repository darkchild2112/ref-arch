using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Ref.Arch.API.Test;

public abstract class IntegrationTest : IDisposable
{
    private bool _disposed = false;
    protected HttpClient Client { get; }

    public IntegrationTest()
    {
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

    protected Dictionary<string, string?> GetConfigurationsToOverride() => [];

    protected void SetupTestServices(IServiceCollection services) { }

    private static string AppConfigPath => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "appsettings.json");

    private void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            Client.Dispose();
        }

        _disposed = true;
    }
}
