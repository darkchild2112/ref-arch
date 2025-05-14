using FluentValidation;

namespace Ref.Arch.Api.Endpoints.Todos.Post;

public class PostTodoRequestValidator : AbstractValidator<PostTodoRequest>
{
    public PostTodoRequestValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("User Id must be a positive number");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title cannot be empty")
            .MaximumLength(250).WithMessage("Title cannot exceed 250 characters");
    }
}
