using FluentValidation;

namespace Rating.Application.Commands.RatingSubmit;

public class RatingSubmitCommandHandlerValidator : AbstractValidator<RatingSubmitCommand>
{
    public RatingSubmitCommandHandlerValidator()
    {
        RuleFor(x => x.ServiceProviderId)
            .NotEmpty().WithMessage("ServiceProviderId is required.");

        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("CustomerId is required.");

        RuleFor(x => x.Score)
            .InclusiveBetween(1, 5).WithMessage("Score must be between 1 and 5.");

        RuleFor(x => x.Comment)
            .MaximumLength(500).WithMessage("Comment must not exceed 500 characters.");
    }
}