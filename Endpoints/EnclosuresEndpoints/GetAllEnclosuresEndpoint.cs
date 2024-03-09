using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using ZooAnimalManagement.API.Contracts.Responses;
using ZooAnimalManagement.API.Mapping;
using ZooAnimalManagement.API.Services;

namespace ZooAnimalManagement.API.Endpoints.EnclosuresEndpoints
{
    [HttpGet("enclosures"), AllowAnonymous]
    public class GetAllEnclosuresEndpoint : EndpointWithoutRequest<GetAllEnclosuresResponse>
    {
        private readonly IEnclosureService _enclosureService;

        public GetAllEnclosuresEndpoint(IEnclosureService enclosureService)
        {
            _enclosureService = enclosureService;
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var enclosures = await _enclosureService.GetAllAsync();
            var enclosuresResponse = enclosures.ToEnclosuresResponse();
            await SendOkAsync(enclosuresResponse, ct);
        }
    }
}
