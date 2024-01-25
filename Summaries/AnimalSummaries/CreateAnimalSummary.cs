using FastEndpoints;
using ZooAnimalManagement.API.Contracts.Responses;
using ZooAnimalManagement.API.Endpoints.AnimalsEndpoints;

namespace ZooAnimalManagement.API.Summaries.AnimalSummaries
{
    public class CreateAnimalSummary : Summary<CreateAnimalEndpoint>
    {
        public CreateAnimalSummary()
        {
            Summary = "Creates a new animal in the system";
            Description = "Creates a new animal in the system";
            Response<AnimalResponse>(201, "Animal was successfully created");
            Response<ValidationFailureResponse>(400, "The request did not pass validation checks");
        }
    }
}
