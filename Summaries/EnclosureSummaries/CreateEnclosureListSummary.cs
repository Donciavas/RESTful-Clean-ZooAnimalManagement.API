using FastEndpoints;
using ZooAnimalManagement.API.Contracts.Responses;
using ZooAnimalManagement.API.Endpoints.EnclosuresEndpoints;

namespace ZooAnimalManagement.API.Summaries.EnclosureSummaries
{
    public class CreateEnclosureListSummary : Summary<CreateEnclosuresListEndpoint>
    {
        public CreateEnclosureListSummary()
        {
            Summary = "Creates new enclosures in the system";
            Description = "Creates new enclosures in the system";
            Response<List<EnclosureResponse>>(200, "Enclosure were successfully created");
            Response<ValidationFailureResponse>(400, "The request did not pass validation checks");
        }
    }
}
