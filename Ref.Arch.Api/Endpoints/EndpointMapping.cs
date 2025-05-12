using Ref.Arch.Api.Endpoints.Todos.Dtos;
using Ref.Arch.Api.Endpoints.Todos.Get;

namespace Ref.Arch.Api.Endpoints;

public static class EndpointMapping
{
    public static RouteGroupBuilder MapTodosEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/",
            async (GetTodosHandler handler, CancellationToken cancellationToken) => await handler.HandleAsync(cancellationToken))
        .Produces<IEnumerable<TodoDto>>()
        .WithName("GetAllTodos");

        group.MapGet("/{id}",
            async (int id, GetTodosHandler handler, CancellationToken cancellationToken) => await handler.HandleAsync(id, cancellationToken))
        .Produces<TodoDto>()
        .WithName("GetTodo");

        return group;
    }
}
