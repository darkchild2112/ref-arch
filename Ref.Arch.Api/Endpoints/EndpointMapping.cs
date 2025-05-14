using Ref.Arch.Api.Endpoints.Todos.Delete;
using Ref.Arch.Api.Endpoints.Todos.Dtos;
using Ref.Arch.Api.Endpoints.Todos.Get;
using Ref.Arch.Api.Endpoints.Todos.Post;

namespace Ref.Arch.Api.Endpoints;

public static class EndpointMapping
{
    public static RouteGroupBuilder MapTodosEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/",
            async (GetTodoHandler handler, CancellationToken cancellationToken) => await handler.HandleAsync(cancellationToken))
        .Produces<IEnumerable<TodoDto>>()
        .WithName("GetAllTodos");

        group.MapGet("/{id}",
            async (int id, GetTodoHandler handler, CancellationToken cancellationToken) => await handler.HandleAsync(id, cancellationToken))
        .Produces<TodoDto>()
        .WithName("GetTodo");

        group.MapPost("/",
            async (PostTodoRequest request, PostTodoHandler handler, CancellationToken cancellationToken) => await handler.HandleAsync(request, cancellationToken))
        .Produces<int>()
        .WithName("PostTodo");

        group.MapDelete("/{id}",
            async (int id, DeleteTodoHandler handler, CancellationToken cancellationToken) => await handler.HandleAsync(id, cancellationToken))
        .WithName("DeleteTodo");

        return group;
    }
}
