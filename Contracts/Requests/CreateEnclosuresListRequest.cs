namespace ZooAnimalManagement.API.Contracts.Requests
{
    public class CreateEnclosuresListRequest
    {
        public IEnumerable<CreateEnclosureRequest> Enclosures { get; init; } = Enumerable.Empty<CreateEnclosureRequest>();
    }
}
