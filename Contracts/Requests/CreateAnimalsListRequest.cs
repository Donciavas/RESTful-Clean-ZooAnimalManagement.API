namespace ZooAnimalManagement.API.Contracts.Requests
{
    public class CreateAnimalsListRequest
    {
        public List<CreateAnimalRequest> Animals { get; init; } = default!;
    }
}
