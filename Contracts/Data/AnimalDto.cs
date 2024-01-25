namespace ZooAnimalManagement.API.Contracts.Data
{
    public class AnimalDto
    {
        public string Id { get; init; } = default!;
        public string Species { get; init; } = default!;
        public string Food { get; init; } = default!;
        public int Amount { get; init; } = default!;
        public string? EnclosureId { get; internal set; } = default!;
    }
}
