using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using ZooAnimalManagement.API.Contracts.Requests;
using ZooAnimalManagement.API.Contracts.Responses;
using ZooAnimalManagement.API.Mapping;
using ZooAnimalManagement.API.Services;

namespace ZooAnimalManagement.API.Endpoints.AnimalsEndpoints
{
    [HttpPut("enclosures/{id:guid}"), AllowAnonymous]
    public class UpdateEnclosureEndpoint : Endpoint<UpdateEnclosureRequest, EnclosureResponse>
    {
        private readonly IEnclosureService _enclosureService;

        public UpdateEnclosureEndpoint(IEnclosureService enclosureService)
        {
            _enclosureService = enclosureService;
        }

        public override async Task HandleAsync(UpdateEnclosureRequest req, CancellationToken ct)
        {
            var existingEnclosure = await _enclosureService.GetAsync(req.Id);

            if (existingEnclosure is null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            var enclosure = req.ToEnclosure();
            await _enclosureService.UpdateAsync(enclosure);

            var enclosureResponse = enclosure.ToEnclosureResponse();
            await SendOkAsync(enclosureResponse, ct);
        }
    }
}
