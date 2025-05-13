namespace Ref.Arch.Api.Clients.JsonPlaceholder;

public record GetTodoResponse(IEnumerable<TodoResponse> Todos);

public record TodoResponse(int UserId, int Id, string Title, bool Completed);
