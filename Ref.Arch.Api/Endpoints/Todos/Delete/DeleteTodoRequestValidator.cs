using FluentValidation;

namespace Ref.Arch.Api.Endpoints.Todos.Delete;

public class DeleteTodoRequestValidator : AbstractValidator<int>
{
    public DeleteTodoRequestValidator()
    {
        RuleFor(x => x)
            .GreaterThan(0).WithMessage("User Id must be a positive number");
    }
}
