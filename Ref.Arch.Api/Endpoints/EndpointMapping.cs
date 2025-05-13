using Ref.Arch.Api.Endpoints.Todos.Dtos;
using Ref.Arch.Api.Endpoints.Todos.Get;
using Ref.Arch.Api.Endpoints.Todos.Post;

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

        group.MapPost("/",
            async (PostTodosRequest request, PostTodosHandler handler, CancellationToken cancellationToken) => await handler.HandleAsync(request, cancellationToken))
        .Produces<TodoDto>()
        .WithName("PostTodo");

        return group;
    }
}
