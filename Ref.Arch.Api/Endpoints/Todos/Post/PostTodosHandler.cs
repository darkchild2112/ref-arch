using Ref.Arch.Api.Clients.JsonPlaceholder;

namespace Ref.Arch.Api.Endpoints.Todos.Post;

public class PostTodosHandler
{
    private readonly JsonPlaceholderClient _client;

    public PostTodosHandler(JsonPlaceholderClient client)
    {
        _client = client;
    }

    public async Task<IResult> HandleAsync(PostTodosRequest request, CancellationToken cancellationToken)
    {
        var todoId = await _client.CreateTodoAsync(request.UserId, request.Title, request.Completed, cancellationToken);

        return Results.Created($"api/v1/todos/{todoId}", todoId);
    }
}
