using FastEndpoints;
using ZooAnimalManagement.API.Contracts.Responses;
using ZooAnimalManagement.API.Endpoints.AnimalsEndpoints;

namespace ZooAnimalManagement.API.Summaries.AnimalSummaries
{
    public class CreateAnimalListSummary : Summary<CreateAnimalsListEndpoint>
    {
        public CreateAnimalListSummary()
        {
            Summary = "Creates new animals in the system";
            Description = "Creates new animals in the system";
            Response<List<AnimalResponse>>(200, "Animals were successfully created");
            Response<ValidationFailureResponse>(400, "The request did not pass validation checks");
        }
    }
}
