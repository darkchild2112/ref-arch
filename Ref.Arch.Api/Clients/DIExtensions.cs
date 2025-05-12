using Microsoft.Extensions.Options;

namespace Ref.Arch.Api.Clients;

public static class DIExtensions
{
    public static void AddJsonPlaceholderClient(this IServiceCollection services, IConfiguration config)
    {
        services.AddOptions<JsonPlaceholderConfig>()
            .Bind(config.GetSection("JsonPlaceholder"));
        services.AddHttpClient<JsonPlaceholderClient>()
            .ConfigureHttpClient((sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<JsonPlaceholderConfig>>();
                client.BaseAddress = new Uri(options.Value.Url);
            });
            //.AddPolicyHandlerFromRegistry(ResiliencyPolicyNames.HttpResiliencyPolicy);

        // Needs Polly
    }
}
