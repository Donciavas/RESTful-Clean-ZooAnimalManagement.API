using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using ZooAnimalManagement.API.Contracts.Requests;
using ZooAnimalManagement.API.Services;

namespace ZooAnimalManagement.API.Endpoints.AnimalsEndpoints
{
    [HttpDelete("enclosures/{id:guid}"), AllowAnonymous]
    public class DeleteEnclosureEndpoint : Endpoint<DeleteEnclosureRequest>
    {
        private readonly IEnclosureService _enclosureService;

        public DeleteEnclosureEndpoint(IEnclosureService enclosureService)
        {
            _enclosureService = enclosureService;
        }

        public override async Task HandleAsync(DeleteEnclosureRequest req, CancellationToken ct)
        {
            var deleted = await _enclosureService.DeleteEnclosureAndObjectsAsync(req.Id);
            if (!deleted)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            await SendNoContentAsync(ct);
        }
    }
}
