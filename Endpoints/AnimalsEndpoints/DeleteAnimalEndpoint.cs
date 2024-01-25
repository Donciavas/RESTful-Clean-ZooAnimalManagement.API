using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using ZooAnimalManagement.API.Contracts.Requests;
using ZooAnimalManagement.API.Services;

namespace ZooAnimalManagement.API.Endpoints.AnimalsEndpoints
{
    [HttpDelete("animals/{id:guid}"), AllowAnonymous]
    public class DeleteAnimalEndpoint : Endpoint<DeleteAnimalRequest>
    {
        private readonly IAnimalService _animalService;

        public DeleteAnimalEndpoint(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        public override async Task HandleAsync(DeleteAnimalRequest req, CancellationToken ct)
        {
            var deleted = await _animalService.DeleteAsync(req.Id);
            if (!deleted)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            await SendNoContentAsync(ct);
        }
    }
}
