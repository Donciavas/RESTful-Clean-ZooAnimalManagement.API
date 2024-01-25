using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using ZooAnimalManagement.API.Contracts.Requests;
using ZooAnimalManagement.API.Mapping;
using ZooAnimalManagement.API.Services;

namespace ZooAnimalManagement.API.Endpoints.AnimalsEndpoints
{
    [HttpPost("animals/list"), AllowAnonymous]
    public class CreateAnimalsListEndpoint : Endpoint<CreateAnimalsListRequest, bool>
    {
        private readonly IAnimalService _animalService;

        public CreateAnimalsListEndpoint(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        public override async Task HandleAsync(CreateAnimalsListRequest request, CancellationToken ct)
        {
            foreach (var animalDto in request.Animals)
            {
                var animal = animalDto.ToAnimal();

                await _animalService.CreateAsync(animal);
            }

            await SendOkAsync(true, ct);
        }
    }
}
