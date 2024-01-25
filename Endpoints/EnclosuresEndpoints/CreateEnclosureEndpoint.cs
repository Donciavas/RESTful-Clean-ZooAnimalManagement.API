using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using ZooAnimalManagement.API.Contracts.Requests;
using ZooAnimalManagement.API.Contracts.Responses;
using ZooAnimalManagement.API.Mapping;
using ZooAnimalManagement.API.Services;

namespace ZooAnimalManagement.API.Endpoints.AnimalsEndpoints
{
    [HttpPost("enclosures"), AllowAnonymous]
    public class CreateEnclosureEndpoint : Endpoint<CreateEnclosureRequest, EnclosureResponse>
    {
        private readonly IEnclosureService _enclosureService;

        public CreateEnclosureEndpoint(IEnclosureService enclosureService)
        {
            _enclosureService = enclosureService;
        }

        public override async Task HandleAsync(CreateEnclosureRequest req, CancellationToken ct)
        {
            var enclosure = req.ToEnclosure();

            await _enclosureService.CreateAsync(enclosure);

            var enclosureResponse = enclosure.ToEnclosureResponse();
            await SendCreatedAtAsync<GetEnclosureEndpoint>(
                new { Id = enclosure.Id.Value }, enclosureResponse, generateAbsoluteUrl: true, cancellation: ct);
        }
    }
}
