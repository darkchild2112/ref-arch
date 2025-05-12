using System.Text.Json;

namespace Ref.Arch.Api.Clients.JsonPlaceholder;

public class JsonPlaceholderClient
{
    private readonly HttpClient _httpClient;

    private readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public JsonPlaceholderClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<TodoResponse>> GetAllTodosAsync(CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync("/todos", cancellationToken);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        var todos = JsonSerializer.Deserialize<IEnumerable<TodoResponse>>(content, JsonOptions);

        return todos ?? [];
    }

    public async Task<TodoResponse?> GetTodoAsync(int id, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync($"/todos/{id}", cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return default;
        
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        var todo = JsonSerializer.Deserialize<TodoResponse>(content, JsonOptions);

        return todo!;
    }
}
