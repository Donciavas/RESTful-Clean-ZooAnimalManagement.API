namespace ZooAnimalManagement.API.Contracts.Responses
{
    public class GetAllEnclosuresResponse
    {
        public IEnumerable<EnclosureResponse> Enclosures { get; init; } = Enumerable.Empty<EnclosureResponse>();
    }
}
