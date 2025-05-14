namespace Ref.Arch.Api.Endpoints.Todos.Post;

public record PostTodoRequest(int UserId, string Title, bool Completed);
