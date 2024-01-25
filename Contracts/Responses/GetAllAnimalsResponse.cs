namespace ZooAnimalManagement.API.Contracts.Responses
{
    public class GetAllAnimalsResponse
    {
        public IEnumerable<AnimalResponse> Animals { get; init; } = Enumerable.Empty<AnimalResponse>();
    }
}
