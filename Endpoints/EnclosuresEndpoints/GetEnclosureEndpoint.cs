using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using ZooAnimalManagement.API.Contracts.Requests;
using ZooAnimalManagement.API.Contracts.Responses;
using ZooAnimalManagement.API.Mapping;
using ZooAnimalManagement.API.Services;

namespace ZooAnimalManagement.API.Endpoints.EnclosuresEndpoints
{
    [HttpGet("enclosures/{id:guid}"), AllowAnonymous]
    public class GetEnclosureEndpoint : Endpoint<GetEnclosureRequest, EnclosureResponse>
    {
        private readonly IEnclosureService _enclosureService;

        public GetEnclosureEndpoint(IEnclosureService enclosureService)
        {
            _enclosureService = enclosureService;
        }

        public override async Task HandleAsync(GetEnclosureRequest req, CancellationToken ct)
        {
            var enclosure = await _enclosureService.GetAsync(req.Id);

            if (enclosure is null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            var enclosureResponse = enclosure.ToEnclosureResponse();
            await SendOkAsync(enclosureResponse, ct);
        }
    }
}
