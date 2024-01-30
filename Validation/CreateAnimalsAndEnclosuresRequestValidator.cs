using FluentValidation;
using ZooAnimalManagement.API.Contracts.Requests;

namespace ZooAnimalManagement.API.Validation
{
    public class CreateAnimalsAndEnclosuresRequestValidator : AbstractValidator<CreateAnimalsAndEnclosuresRequest>
    {
        public CreateAnimalsAndEnclosuresRequestValidator()
        {
            RuleFor(x => x.Animals)
                .NotEmpty()
                .ForEach(animal => animal.NotEmpty());

            RuleFor(x => x.Enclosures)
                .NotEmpty()
                .ForEach(enclosure => enclosure.NotEmpty());

            RuleFor(x => x.Animals)
                .Must(animalList => animalList.All(animal => !string.IsNullOrWhiteSpace(animal.Species)))
                .WithMessage("All animals must have a non-null and non-empty species.");

            RuleFor(x => x.Animals)
                .Must(animalList => animalList.All(animal => !string.IsNullOrWhiteSpace(animal.Food)))
                .WithMessage("All food types must have a non-null and non-empty types.");

            RuleFor(x => x.Animals)
                .Must(animalList => animalList.All(animal => !int.IsPositive(animal.Amount)))
                .WithMessage("All animals amount entry must have a positive integer numbers.");

            RuleFor(x => x.Enclosures)
                .Must(enclosureList => enclosureList.All(enclosure => !string.IsNullOrWhiteSpace(enclosure.Name)))
                .WithMessage("All enclosures must have a non-null and non-empty names.");

            RuleFor(x => x.Enclosures)
                .Must(enclosureList => enclosureList.All(enclosure => !string.IsNullOrWhiteSpace(enclosure.Size)))
                .WithMessage("All enclosures must have a non-null and non-empty sizes.");

            RuleFor(x => x.Enclosures)
                .Must(enclosureList => enclosureList.All(enclosure => !string.IsNullOrWhiteSpace(enclosure.Location)))
                .WithMessage("All enclosures must have a non-null and non-empty locations.");

            RuleFor(x => x.Enclosures)
                .Must(enclosureList => enclosureList.All(enclosure => enclosure.Objects != null && enclosure.Objects.Any()))
                .WithMessage("All enclosures must have a non-null and non-empty objects.");
        }
    }
}
