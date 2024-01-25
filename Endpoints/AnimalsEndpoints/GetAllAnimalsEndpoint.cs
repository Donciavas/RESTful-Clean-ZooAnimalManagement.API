using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using ZooAnimalManagement.API.Contracts.Responses;
using ZooAnimalManagement.API.Mapping;
using ZooAnimalManagement.API.Services;

namespace ZooAnimalManagement.API.Endpoints.AnimalsEndpoints
{
    [HttpGet("animals"), AllowAnonymous]
    public class GetAllAnimalsEndpoint : EndpointWithoutRequest<GetAllAnimalsResponse>
    {
        private readonly IAnimalService _animalService;

        public GetAllAnimalsEndpoint(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var animals = await _animalService.GetAllAsync();
            var animalsResponse = animals.ToAnimalsResponse();
            await SendOkAsync(animalsResponse, ct);
        }
    }
}
