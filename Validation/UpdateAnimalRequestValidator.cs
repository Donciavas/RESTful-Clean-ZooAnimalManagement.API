using FluentValidation;
using ZooAnimalManagement.API.Contracts.Requests;

namespace ZooAnimalManagement.API.Validation
{
    public class UpdateAnimalRequestValidator : AbstractValidator<UpdateAnimalRequest>
    {
        public UpdateAnimalRequestValidator()
        {
            RuleFor(x => x.Species).NotEmpty();
            RuleFor(x => x.Food).NotEmpty();
            RuleFor(x => x.Amount).NotEmpty().GreaterThan(0);
        }
    }
}
