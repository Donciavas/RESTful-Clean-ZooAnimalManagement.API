using ZooAnimalManagement.API.Domain;

namespace ZooAnimalManagement.API.Contracts.Data
{
    public class EnclosureDto
    {
        public string Id { get; init; } = default!;
        public string Name { get; init; } = default!;
        public string Size { get; init; } = default!;
        public string Location { get; init; } = default!;
        public List<string> Objects { get; init; } = default!;
        public List<AnimalDto> Animals { get; internal set; } = new();
    }
}
