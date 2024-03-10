using ZooAnimalManagement.API.Contracts.Responses;

namespace ZooAnimalManagement.API.Contracts.Requests
{
    public class CreateAnimalsListRequest
    {
        public IEnumerable<CreateAnimalRequest> Animals { get; init; } = Enumerable.Empty<CreateAnimalRequest>();
    }
}
