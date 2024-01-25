using FastEndpoints;
using ZooAnimalManagement.API.Contracts.Responses;
using ZooAnimalManagement.API.Endpoints.AnimalsEndpoints;

namespace ZooAnimalManagement.API.Summaries
{
    public class GetAllEnclosuresSummary : Summary<GetAllEnclosuresEndpoint>
    {
        public GetAllEnclosuresSummary()
        {
            Summary = "Returns all the enclosures in the system";
            Description = "Returns all the enclosures in the system";
            Response<GetAllEnclosuresResponse>(200, "All enclosures in the system are returned");
        }
    }
}
