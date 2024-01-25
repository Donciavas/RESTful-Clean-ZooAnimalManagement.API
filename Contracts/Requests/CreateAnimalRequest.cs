namespace ZooAnimalManagement.API.Contracts.Requests
{
    public class CreateAnimalRequest
    {
        public string Species { get; init; } = default!;
        public string Food { get; init; } = default!;
        public int Amount { get; init; } = default!;
        public string? EnclosureId { get; init; } = default!;
    }
}
