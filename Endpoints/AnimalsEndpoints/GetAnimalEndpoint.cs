using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using ZooAnimalManagement.API.Contracts.Requests;
using ZooAnimalManagement.API.Contracts.Responses;
using ZooAnimalManagement.API.Mapping;
using ZooAnimalManagement.API.Services;

namespace ZooAnimalManagement.API.Endpoints.AnimalsEndpoints
{
    [HttpGet("animals/{id:guid}"), AllowAnonymous]
    public class GetAnimalEndpoint : Endpoint<GetAnimalRequest, AnimalResponse>
    {
        private readonly IAnimalService _animalService;

        public GetAnimalEndpoint(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        public override async Task HandleAsync(GetAnimalRequest req, CancellationToken ct)
        {
            var animal = await _animalService.GetAsync(req.Id);

            if (animal is null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            var animalResponse = animal.ToAnimalResponse();
            await SendOkAsync(animalResponse, ct);
        }
    }
}
