using ZooAnimalManagement.API.Domain.Common.Animal;

namespace ZooAnimalManagement.API.Domain
{
    public class Animal
    {
        public AnimalId Id { get; init; } = AnimalId.From(Guid.NewGuid());
        public Species Species { get; init; } = default!;
        public Food Food { get; init; } = default!;
        public Amount Amount { get; init; } = default!;
        public EnclosureId EnclosureId { get; internal set; } = default!;      
    }
}
