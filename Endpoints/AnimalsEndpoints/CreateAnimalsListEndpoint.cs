using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using ZooAnimalManagement.API.Contracts.Requests;
using ZooAnimalManagement.API.Contracts.Responses;
using ZooAnimalManagement.API.Mapping;
using ZooAnimalManagement.API.Services;

namespace ZooAnimalManagement.API.Endpoints.AnimalsEndpoints
{
    [HttpPost("animals/list"), AllowAnonymous]
    public class CreateAnimalsListEndpoint : Endpoint<CreateAnimalsListRequest, List<AnimalResponse>>
    {
        private readonly IAnimalService _animalService;

        public CreateAnimalsListEndpoint(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        public override async Task HandleAsync(CreateAnimalsListRequest request, CancellationToken ct)
        {
            var animalResponses = new List<AnimalResponse>();

            foreach (var animalReq in request.Animals)
            {
                var animal = animalReq.ToAnimal();

                await _animalService.CreateAsync(animal);

                animalResponses.Add(animal.ToAnimalResponse());
            }

            await SendOkAsync(animalResponses, ct);
        }
    }
}
