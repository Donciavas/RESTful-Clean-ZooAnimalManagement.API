using FastEndpoints;
using ZooAnimalManagement.API.Contracts.Responses;
using ZooAnimalManagement.API.Endpoints.AnimalsEndpoints;

namespace ZooAnimalManagement.API.Summaries
{
    public class GetEnclosureSummary : Summary<GetEnclosureEndpoint>
    {
        public GetEnclosureSummary()
        {
            Summary = "Returns a single enclosure by id";
            Description = "Returns a single enclosure by id";
            Response<GetAllEnclosuresResponse>(200, "Successfully found and returned the enclosure");
            Response(404, "The enclosure does not exist in the system");
        }
    }
}
