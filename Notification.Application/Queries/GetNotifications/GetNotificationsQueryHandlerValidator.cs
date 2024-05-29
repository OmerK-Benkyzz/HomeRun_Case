using FluentValidation;

namespace Notification.Application.Queries.GetNotifications;

public class GetNotificationsQueryHandlerValidator : AbstractValidator<GetNotificationsQuery>
{
    public GetNotificationsQueryHandlerValidator()
    {
        RuleFor(x => x.ServiceProviderId)
            .NotEmpty().WithMessage("ServiceProviderId cannot be empty.");

        RuleFor(x => x.PageIndex)
            .GreaterThanOrEqualTo(0).WithMessage("PageIndex must be greater than or equal to 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("PageSize must be greater than 0.");
    }
}