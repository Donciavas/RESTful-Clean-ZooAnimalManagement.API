using FastEndpoints;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using ZooAnimalManagement.API.Contracts.Responses;
using ZooAnimalManagement.API.Mapping;
using ZooAnimalManagement.API.Services;

namespace ZooAnimalManagement.API.Endpoints
{
    [HttpPost("Transfer animals to enclosures"), AllowAnonymous]
    public class TransferAnimalsToEnclosuresEndpoint : EndpointWithoutRequest<GetAllEnclosuresResponse>
    {
        private readonly IAnimalService _animalService;
        private readonly IEnclosureService _enclosureService;
        private readonly IEnclosureAssignmentService _enclosureAssignmentService;

        public TransferAnimalsToEnclosuresEndpoint(IAnimalService animalService, IEnclosureService enclosureService, IEnclosureAssignmentService enclosureAssignmentService)
        {
            _animalService = animalService;
            _enclosureService = enclosureService;
            _enclosureAssignmentService = enclosureAssignmentService;
        }

        public override async Task HandleAsync(CancellationToken ct)
        {            
            var animalList = await _animalService.GetAllAsync();

            var enclosureList = await _enclosureService.GetAllAsync();

            if (animalList.Any() && enclosureList.Any())
            {
                await _enclosureAssignmentService.AssignAllAnimalsToEnclosuresAsync(animalList.ToList(), enclosureList.ToList());

                var filledEnclosures = await _enclosureService.GetAllAsync();
                var filledEnclosuresResponse = filledEnclosures.ToEnclosuresResponse();

                await SendOkAsync(filledEnclosuresResponse, ct);
            }
            else
            {
                var message = $"There are not enough resources to process animal transfer. Number of animal species: {animalList.Count()}, enclosures: {enclosureList.Count()}";
                throw new ValidationException(message, new[]
                {
                new ValidationFailure(null, message)
            });
            }
        }
    }
}
