using FastEndpoints;
using ZooAnimalManagement.API.Endpoints.AnimalsEndpoints;

namespace ZooAnimalManagement.API.Summaries.AnimalSummaries
{
    public class DeleteAnimalSummary : Summary<DeleteAnimalEndpoint>
    {
        public DeleteAnimalSummary()
        {
            Summary = "Deleted a animal in the system";
            Description = "Deleted a animal in the system";
            Response(204, "The animal was deleted successfully");
            Response(404, "The animal was not found in the system");
        }
    }
}
