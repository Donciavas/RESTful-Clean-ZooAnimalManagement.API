using ZooAnimalManagement.API.Contracts.Requests;
using ZooAnimalManagement.API.Domain;
using ZooAnimalManagement.API.Domain.Common.Animal;
using ZooAnimalManagement.API.Domain.Common.Enclosure;

namespace ZooAnimalManagement.API.Mapping
{
    public static class ApiContractToDomainMapper
    {
        public static Animal ToAnimal(this CreateAnimalRequest request)
        {
            return new Animal
            {
                Id = AnimalId.From(Guid.NewGuid()),
                Species = Species.From(request.Species),
                Food = Food.From(request.Food),
                Amount = Amount.From(request.Amount),
                EnclosureId = !string.IsNullOrEmpty(request.EnclosureId)
                        ? EnclosureId.From(Guid.Parse(request.EnclosureId))
                        : default!
            };
        }

        public static Animal ToAnimal(this UpdateAnimalRequest request)
        {
            return new Animal
            {
                Id = AnimalId.From(request.Id),
                Species = Species.From(request.Species),
                Food = Food.From(request.Food),
                Amount = Amount.From(request.Amount),
                EnclosureId = EnclosureId.From(request.EnclosureId)
            };
        }

        public static Enclosure ToEnclosure(this CreateEnclosureRequest request)
        {
            return new Enclosure
            {
                Id = EnclosureId.From(Guid.NewGuid()),
                Name = Name.From(request.Name),
                Size = Size.FromString(request.Size),
                Location = Location.From(request.Location),
                Objects = EnclosureObjects.From(request.Objects).Value,
                Animals = request.Animals
            };
        }

        public static Enclosure ToEnclosure(this UpdateEnclosureRequest request)
        {
            return new Enclosure
            {
                Id = EnclosureId.From(request.Id),
                Name = Name.From(request.Name),
                Size = Size.FromString(request.Size),
                Location = Location.From(request.Location),
                Objects = EnclosureObjects.From(request.Objects).Value,
                Animals = request.Animals
            };
        }
    }
}
