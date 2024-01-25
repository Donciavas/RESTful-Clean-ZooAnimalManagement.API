using ZooAnimalManagement.API.Contracts.Data;
using ZooAnimalManagement.API.Domain;
using ZooAnimalManagement.API.Domain.Common.Animal;
using ZooAnimalManagement.API.Domain.Common.Enclosure;

namespace ZooAnimalManagement.API.Mapping
{
    public static class DtoToDomainMapper
    {
        public static Animal ToAnimal(this AnimalDto animalDto)
        {
            return new Animal
            {
                Id = AnimalId.From(Guid.Parse(animalDto.Id)),
                Species = Species.From(animalDto.Species),
                Food = Food.From(animalDto.Food),
                Amount = Amount.From(animalDto.Amount),
                EnclosureId = EnclosureId.From(animalDto.EnclosureId != null ? Guid.Parse(animalDto.EnclosureId) : null) // null here is required, because default creates 00000000-0000-0000-0000-000000000000 Guids 
            };
        }

        public static Enclosure ToEnclosure(this EnclosureDto enclosureDto)
        {
            return new Enclosure
            {
                Id = EnclosureId.From(Guid.Parse(enclosureDto.Id)),
                Name = Name.From(enclosureDto.Name),
                Size = Size.FromString(enclosureDto.Size),
                Location = Location.From(enclosureDto.Location),
                Objects = EnclosureObjects.From(enclosureDto.Objects).Value,
                Animals = enclosureDto.Animals.Select(animal => animal.ToAnimal()).ToList()
            };
        }
    }
}
