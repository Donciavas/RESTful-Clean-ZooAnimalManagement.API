using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using ZooAnimalManagement.API.Contracts.Requests;
using ZooAnimalManagement.API.Contracts.Responses;
using ZooAnimalManagement.API.Mapping;
using ZooAnimalManagement.API.Services;

namespace ZooAnimalManagement.API.Endpoints.AnimalsEndpoints
{
    [HttpPut("animals/{id:guid}"), AllowAnonymous]
    public class UpdateAnimalEndpoint : Endpoint<UpdateAnimalRequest, AnimalResponse>
    {
        private readonly IAnimalService _animalService;

        public UpdateAnimalEndpoint(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        public override async Task HandleAsync(UpdateAnimalRequest req, CancellationToken ct)
        {
            var existingAnimal = await _animalService.GetAsync(req.Id);

            if (existingAnimal is null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            var animal = req.ToAnimal();
            await _animalService.UpdateAsync(animal);

            var animalResponse = animal.ToAnimalResponse();
            await SendOkAsync(animalResponse, ct);
        }
    }
}
