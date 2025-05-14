using FluentValidation;
using Ref.Arch.Api.Clients.JsonPlaceholder;

namespace Ref.Arch.Api.Endpoints.Todos.Delete;

public class DeleteTodoHandler
{
    private readonly JsonPlaceholderClient _client;
    private readonly IValidator<int> _requestValidator;

    public DeleteTodoHandler(JsonPlaceholderClient client, IValidator<int> requestValidator)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client), "must not be null");
        _requestValidator = requestValidator ?? throw new ArgumentNullException(nameof(requestValidator), "must not be null");
    }

    public async Task<IResult> HandleAsync(int id, CancellationToken cancellationToken)
    {
        var validationResult = await _requestValidator.ValidateAsync(id, cancellationToken);

        if (!validationResult.IsValid)
            return Results.ValidationProblem(validationResult.ToDictionary());

        await _client.DeleteTodoAsync(id, cancellationToken);

        return Results.Ok();
    }
}
