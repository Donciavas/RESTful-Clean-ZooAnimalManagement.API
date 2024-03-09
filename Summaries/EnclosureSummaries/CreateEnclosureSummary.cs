using FastEndpoints;
using ZooAnimalManagement.API.Contracts.Responses;
using ZooAnimalManagement.API.Endpoints.EnclosuresEndpoints;

namespace ZooAnimalManagement.API.Summaries.EnclosureSummaries
{
    public class CreateEnclosureSummary : Summary<CreateEnclosureEndpoint>
    {
        public CreateEnclosureSummary()
        {
            Summary = "Creates a new enclosure in the system";
            Description = "Creates a new enclosure in the system";
            Response<EnclosureResponse>(201, "Enclosure was successfully created");
            Response<ValidationFailureResponse>(400, "The request did not pass validation checks");
        }
    }
}
