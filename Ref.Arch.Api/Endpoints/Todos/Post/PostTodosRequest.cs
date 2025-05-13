namespace Ref.Arch.Api.Endpoints.Todos.Post;

public record PostTodosRequest(int UserId, string Title, bool Completed);
