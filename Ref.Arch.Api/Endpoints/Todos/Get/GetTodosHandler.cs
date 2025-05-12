using Microsoft.AspNetCore.Http.HttpResults;
using Ref.Arch.Api.Clients;
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
}
