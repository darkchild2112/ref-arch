using FluentValidation;
using Ref.Arch.Api.Clients.JsonPlaceholder;

namespace Ref.Arch.Api.Endpoints.Todos.Post;

public class PostTodoHandler
{
    private readonly JsonPlaceholderClient _client;
    private readonly IValidator<PostTodoRequest> _requestValidator;

    public PostTodoHandler(JsonPlaceholderClient client, IValidator<PostTodoRequest> requestValidator)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client), "must not be null");
        _requestValidator = requestValidator ?? throw new ArgumentNullException(nameof(requestValidator), "must not be null");
    }

    public async Task<IResult> HandleAsync(PostTodoRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _requestValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Results.ValidationProblem(validationResult.ToDictionary());

        var todoId = await _client.CreateTodoAsync(request.UserId, request.Title, request.Completed, cancellationToken);

        return Results.Created($"api/v1/todos/{todoId}", todoId);
    }
}
