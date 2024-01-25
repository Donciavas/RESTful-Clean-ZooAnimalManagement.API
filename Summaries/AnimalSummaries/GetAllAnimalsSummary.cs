using FastEndpoints;
using ZooAnimalManagement.API.Contracts.Responses;
using ZooAnimalManagement.API.Endpoints.AnimalsEndpoints;

namespace ZooAnimalManagement.API.Summaries.AnimalSummaries
{
    public class GetAllAnimalsSummary : Summary<GetAllAnimalsEndpoint>
    {
        public GetAllAnimalsSummary()
        {
            Summary = "Returns all the animals in the system";
            Description = "Returns all the animals in the system";
            Response<GetAllAnimalsResponse>(200, "All animals in the system are returned");
        }
    }
}
