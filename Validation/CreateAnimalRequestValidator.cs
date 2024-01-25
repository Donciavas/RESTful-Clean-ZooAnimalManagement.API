using FluentValidation;
using ZooAnimalManagement.API.Contracts.Requests;

namespace ZooAnimalManagement.API.Validation
{
    public class CreateAnimalRequestValidator : AbstractValidator<CreateAnimalRequest>
    {
        public CreateAnimalRequestValidator()
        {
            RuleFor(x => x.Species).NotEmpty();
            RuleFor(x => x.Food).NotEmpty();
            RuleFor(x => x.Amount).NotEmpty();
        }
    }
}
