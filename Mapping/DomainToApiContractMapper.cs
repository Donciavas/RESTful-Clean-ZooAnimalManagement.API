using ZooAnimalManagement.API.Contracts.Responses;
using ZooAnimalManagement.API.Domain;

namespace ZooAnimalManagement.API.Mapping
{
    public static class DomainToApiContractMapper
    {
        public static AnimalResponse ToAnimalResponse(this Animal animal)
        {
            return new AnimalResponse
            {
                Id = animal.Id.Value,
                Species = animal.Species.Value,
                Food = animal.Food.Value,
                Amount = animal.Amount.Value,
                EnclosureId = animal.EnclosureId?.Value
            };
        }

        public static GetAllAnimalsResponse ToAnimalsResponse(this IEnumerable<Animal> animals)
        {
            return new GetAllAnimalsResponse
            {
                Animals = animals.Select(x => new AnimalResponse
                {
                    Id = x.Id.Value,
                    Species = x.Species.Value,
                    Food = x.Food.Value,
                    Amount = x.Amount.Value,
                    EnclosureId = x.EnclosureId?.Value
                })
            };
        }

        public static EnclosureResponse ToEnclosureResponse(this Enclosure enclosure)
        {
            return new EnclosureResponse
            {
                Id = enclosure.Id.Value,
                Name = enclosure.Name.Value,
                Size = enclosure.Size.Value,
                Location = enclosure.Location.Value,
                Objects = enclosure.Objects,
                Animals = enclosure.Animals.ToList()
            };
        }

        public static GetAllEnclosuresResponse ToEnclosuresResponse(this IEnumerable<Enclosure> enclosures)
        {
            return new GetAllEnclosuresResponse
            {
                Enclosures = enclosures.Select(x => new EnclosureResponse
                {
                    Id = x.Id.Value,
                    Name = x.Name.Value,
                    Size = x.Size.Value,
                    Location = x.Location.Value,
                    Objects = x.Objects,
                    Animals = x.Animals.ToList()
                })
            };
        }
    }
}
