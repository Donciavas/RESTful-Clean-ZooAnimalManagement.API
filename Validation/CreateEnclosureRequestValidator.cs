using FluentValidation;
using ZooAnimalManagement.API.Contracts.Requests;

namespace ZooAnimalManagement.API.Validation
{
    public class CreateEnclosureRequestValidator : AbstractValidator<CreateEnclosureRequest>
    {
        public CreateEnclosureRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Size).NotEmpty();
            RuleFor(x => x.Location).NotEmpty();
            RuleFor(x => x.Objects).NotEmpty().ForEach( ob => ob.NotEmpty());
        }
    }
}
