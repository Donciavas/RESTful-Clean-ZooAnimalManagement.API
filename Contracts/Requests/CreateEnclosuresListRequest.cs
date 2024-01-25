namespace ZooAnimalManagement.API.Contracts.Requests
{
    public class CreateEnclosuresListRequest
    {
        public List<CreateEnclosureRequest> Enclosures { get; init; } = default!;
    }
}
