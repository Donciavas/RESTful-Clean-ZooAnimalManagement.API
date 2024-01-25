using ZooAnimalManagement.API.Contracts.Data;
using ZooAnimalManagement.API.Domain;

namespace ZooAnimalManagement.API.Contracts.Responses
{
    public class EnclosureResponse
    {
        public Guid? Id { get; init; }
        public string Name { get; init; } = default!;
        public string Size { get; init; } = default!;
        public string Location { get; init; } = default!;
        public List<string> Objects { get; init; } = default!;
        public List<Animal> Animals { get; init; } = default!;

    }
}
