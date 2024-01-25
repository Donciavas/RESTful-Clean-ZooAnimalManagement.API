using ZooAnimalManagement.API.Domain.Common.Animal;
using ZooAnimalManagement.API.Domain.Common.Enclosure;

namespace ZooAnimalManagement.API.Domain
{
    public class Enclosure
    {
        public EnclosureId Id { get; init; } = EnclosureId.From(Guid.NewGuid());
        public Name Name { get; init; } = default!;
        public Size Size { get; init; } = default!;
        public Location Location { get; init; } = default!;
        public List<string> Objects { get; init; } = default!;
        public List<Animal> Animals { get; internal set; } = new();

    }
}
