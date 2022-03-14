using FluentValidation;
using TodoApi.Models;

namespace TodoApi.Validators
{
    public class CreateOrUpdateDtoTodoItemValidator : AbstractValidator<CreateOrUpdateTodoItemDto>
    {
        private const int MaximumAllowedLength = 255;

        public CreateOrUpdateDtoTodoItemValidator()
        {
            RuleFor(t => t.Name)
                .NotEmpty()
                .WithMessage("Name should not be empty.")
                .MaximumLength(MaximumAllowedLength)
                .WithMessage("Name exceeds maximum allowed length. Maximum length is {maximumLength}");
        }
    }
}
