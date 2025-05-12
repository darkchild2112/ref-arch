namespace Ref.Arch.Api.Clients;

public record TodoResponse(int UserId, int Id, string Title, bool Completed);
