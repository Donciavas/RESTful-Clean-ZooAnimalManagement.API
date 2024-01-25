using FastEndpoints;
using ZooAnimalManagement.API.Contracts.Responses;
using ZooAnimalManagement.API.Endpoints.AnimalsEndpoints;

namespace ZooAnimalManagement.API.Summaries.AnimalSummaries
{
    public class GetAnimalSummary : Summary<GetAnimalEndpoint>
    {
        public GetAnimalSummary()
        {
            Summary = "Returns a single animal by id";
            Description = "Returns a single animal by id";
            Response<GetAllAnimalsResponse>(200, "Successfully found and returned the animal");
            Response(404, "The animal does not exist in the system");
        }
    }
}
