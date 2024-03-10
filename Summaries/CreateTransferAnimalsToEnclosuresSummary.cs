using FastEndpoints;
using ZooAnimalManagement.API.Contracts.Responses;
using ZooAnimalManagement.API.Endpoints;

namespace ZooAnimalManagement.API.Summaries
{
    public class CreateTransferAnimalsToEnclosuresSummary : Summary<CreateTransferAnimalsToEnclosuresEndpoint>
    {
        public CreateTransferAnimalsToEnclosuresSummary()
        {
            Summary = "Creates and transfers animals to unoccupied enclosures in the system";
            Description = "Creates and transfers animals to unoccupied enclosures in the system";
            Response<GetAllAnimalsResponse>(200, "Animals were successfully created and accommodated");
            Response<ValidationFailureResponse>(400, "Both, animal and enclosure lists, must be provided");
        }
    }
}
