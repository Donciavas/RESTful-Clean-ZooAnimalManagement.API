using FluentValidation;
using ZooAnimalManagement.API.Contracts.Requests;

namespace ZooAnimalManagement.API.Validation
{
    public class UpdateEnclosureRequestValidator : AbstractValidator<UpdateEnclosureRequest>
    {
        public UpdateEnclosureRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Size).NotEmpty();
            RuleFor(x => x.Location).NotEmpty();
            RuleFor(x => x.Objects).NotEmpty().ForEach(ob => ob.NotEmpty());
        }
    }
}
