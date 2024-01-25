using FastEndpoints;
using ZooAnimalManagement.API.Contracts.Responses;
using ZooAnimalManagement.API.Endpoints.AnimalsEndpoints;

namespace ZooAnimalManagement.API.Summaries.AnimalSummaries
{
    public class UpdateAnimalSummary : Summary<UpdateAnimalEndpoint>
    {
        public UpdateAnimalSummary()
        {
            Summary = "Updates an existing animal in the system";
            Description = "Updates an existing animal in the system";
            Response<AnimalResponse>(201, "Animal was successfully updated");
            Response<ValidationFailureResponse>(400, "The request did not pass validation checks");
        }
    }
}
