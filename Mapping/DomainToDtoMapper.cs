using ZooAnimalManagement.API.Contracts.Data;
using ZooAnimalManagement.API.Domain;

namespace ZooAnimalManagement.API.Mapping
{
    public static class DomainToDtoMapper
    {
        public static AnimalDto ToAnimalDto(this Animal animal)
        {
            return new AnimalDto
            {
                Id = animal.Id.Value.ToString(),
                Species = animal.Species.Value,
                Food = animal.Food.Value,
                Amount = animal.Amount.Value,
                EnclosureId = animal.EnclosureId?.Value?.ToString() ?? default!
            };
        }

        public static EnclosureDto ToEnclosureDto(this Enclosure enclosure)
        {
            return new EnclosureDto
            {
                Id = enclosure.Id?.Value?.ToString() ?? default!,
                Name = enclosure.Name.Value,
                Size = enclosure.Size.Value,
                Location = enclosure.Location.Value,
                Objects = enclosure.Objects,
                Animals = enclosure.Animals.Select(animal => animal.ToAnimalDto()).ToList()
            };
        }
    }
}
