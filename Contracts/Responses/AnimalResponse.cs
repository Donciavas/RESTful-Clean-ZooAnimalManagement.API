namespace ZooAnimalManagement.API.Contracts.Responses
{
    public class AnimalResponse
    {
        public Guid Id { get; init; }
        public string Species { get; init; } = default!;
        public string Food { get; init; } = default!;
        public int Amount { get; init; } = default!;
        public Guid? EnclosureId { get; init; } = default!;
    }
}
