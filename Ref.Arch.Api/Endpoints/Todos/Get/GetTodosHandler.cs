using Ref.Arch.Api.Clients.JsonPlaceholder;
using Ref.Arch.Api.Endpoints.Todos.Dtos;

namespace Ref.Arch.Api.Endpoints.Todos.Get;

public sealed class GetTodosHandler
{
    private readonly JsonPlaceholderClient _client;

    public GetTodosHandler(JsonPlaceholderClient client)
    {
        _client = client;
    }

    public async Task<IResult> HandleAsync(CancellationToken cancellationToken)
    {
        var todos = await _client.GetAllTodosAsync(cancellationToken);

        if (todos is null)
            return Results.InternalServerError();

        var todoDtos = todos.Select(t => new TodoDto(t.UserId, t.Id, t.Title, t.Completed));

        return Results.Ok(todoDtos);
    }

    public async Task<IResult> HandleAsync(int id, CancellationToken cancellationToken)
    {
        var todo = await _client.GetTodoAsync(id, cancellationToken);

        if (todo is null)
            return Results.NotFound();

        var todoDto = new TodoDto(todo.UserId, todo.Id, todo.Title, todo.Completed);

        return Results.Ok(todoDto);
    }
}
