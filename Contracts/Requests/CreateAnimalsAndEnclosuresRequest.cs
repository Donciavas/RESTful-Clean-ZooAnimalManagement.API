namespace ZooAnimalManagement.API.Contracts.Requests
{
    public class CreateAnimalsAndEnclosuresRequest
    {
        public List<CreateAnimalRequest> Animals { get; init; } = default!;
        public List<CreateEnclosureRequest> Enclosures { get; init; } = default!;
    }
}
