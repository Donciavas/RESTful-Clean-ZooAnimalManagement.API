using FastEndpoints;
using ZooAnimalManagement.API.Endpoints.AnimalsEndpoints;

namespace ZooAnimalManagement.API.Summaries
{
    public class DeleteEnclosureSummary : Summary<DeleteEnclosureEndpoint>
    {
        public DeleteEnclosureSummary()
        {
            Summary = "Deleted a enclosure the system";
            Description = "Deleted a enclosure the system";
            Response(204, "The enclosure was deleted successfully");
            Response(404, "The enclosure was not found in the system");
        }
    }
}
