using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using ZooAnimalManagement.API.Contracts.Requests;
using ZooAnimalManagement.API.Contracts.Responses;
using ZooAnimalManagement.API.Mapping;
using ZooAnimalManagement.API.Services;

namespace ZooAnimalManagement.API.Endpoints.AnimalsEndpoints
{
    [HttpPost("animals"), AllowAnonymous]
    public class CreateAnimalEndpoint : Endpoint<CreateAnimalRequest, AnimalResponse>
    {
        private readonly IAnimalService _animalService;

        public CreateAnimalEndpoint(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        public override async Task HandleAsync(CreateAnimalRequest req, CancellationToken ct)
        {
            var animal = req.ToAnimal();

            await _animalService.CreateAsync(animal);

            var animalResponse = animal.ToAnimalResponse();
            await SendCreatedAtAsync<GetAnimalEndpoint>(
                new { Id = animal.Id.Value }, animalResponse, generateAbsoluteUrl: true, cancellation: ct);
        }
    }
}
