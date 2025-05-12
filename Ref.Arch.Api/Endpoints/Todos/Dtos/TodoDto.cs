namespace Ref.Arch.Api.Endpoints.Todos.Dtos;

public record TodoDto(int UserId, int Id, string Title, bool Completed);
