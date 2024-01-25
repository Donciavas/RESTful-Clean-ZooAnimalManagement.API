using FluentValidation;
using ZooAnimalManagement.API.Contracts.Requests;

namespace ZooAnimalManagement.API.Validation
{
    public class CreateAnimalsAndEnclosuresRequestValidator : AbstractValidator<CreateAnimalsAndEnclosuresRequest>
    {
        public CreateAnimalsAndEnclosuresRequestValidator()
        {
            RuleFor(x => x.Animals).NotEmpty();
            RuleFor(x => x.Enclosures).NotEmpty();
        }
    }
}
