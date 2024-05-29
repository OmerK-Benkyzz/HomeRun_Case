
using FluentValidation;

namespace Rating.Application.Queries.GetAvarageRating; 

public class GetAverageRateQueryHandlerValidator : AbstractValidator<GetAverageRateQueryHandler>
{
    public GetAverageRateQueryHandlerValidator()
    {
      //Write Rule
     }
}