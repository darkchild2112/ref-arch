using FluentValidation;
using Ref.Arch.Api.Clients.JsonPlaceholder;

namespace Ref.Arch.Api.Endpoints.Todos.Delete;

public class DeleteTodoHandler
{
    private readonly JsonPlaceholderClient _client;
    private readonly IValidator<int> _requestValidator;

    public DeleteTodoHandler(JsonPlaceholderClient client, IValidator<int> requestValidator)
    {
        _client = client;
        _requestValidator = requestValidator;
    }

    public async Task<IResult> HandleAsync(int id, CancellationToken cancellationToken)
    {
        var validationResult = await _requestValidator.ValidateAsync(id);

        if (!validationResult.IsValid)
            return Results.ValidationProblem(validationResult.ToDictionary());

        await _client.DeleteTodoAsync(id, cancellationToken);

        return Results.Ok();
    }
}
