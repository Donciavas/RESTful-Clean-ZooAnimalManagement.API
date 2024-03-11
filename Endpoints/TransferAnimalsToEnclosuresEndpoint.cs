using FastEndpoints;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using ZooAnimalManagement.API.Contracts.Requests;
using ZooAnimalManagement.API.Contracts.Responses;
using ZooAnimalManagement.API.Domain.Common.Animal;
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

            if (!animalList.Any() && !enclosureList.Any())
            {
                var message = $"There are not enough resources to process animal transfer. Number of animal species: {animalList.Count()}, enclosures: {enclosureList.Count()}";
                throw new ValidationException(message, new[]
                {
                new ValidationFailure(null, message)
            });
            }

            var carnivores = animalList
                .Where(animal => animal.Food == Food.From("Carnivore"))
                .ToList();

            var herbivores = animalList
                .Where(animal => animal.Food == Food.From("Herbivore"))
                .ToList();

            // due to fact, that Carnivores cannot be accommodated with herbivores, and 2 different species of Carnivores can be in one enclosure
            bool hasExcessCarnivores = carnivores.Count > enclosureList.Count() * 2;
            bool hasMaxCarnivoresWithHerbivores = carnivores.Count == enclosureList.Count() * 2 && herbivores.Count > 0;
            bool hasNearMaxCarnivoresWithHerbivores = carnivores.Count + 1 == enclosureList.Count() * 2 && herbivores.Count > 0;

            // Case to kill execution fast: Check for excess carnivores, that would violate rules to accommodate animals
            if (hasExcessCarnivores || hasMaxCarnivoresWithHerbivores || hasNearMaxCarnivoresWithHerbivores)
            {
                var message =
                    $"Unable to accommodate animals to enclosures due to insufficient amount of enclosures";
                throw new ValidationException(message, new[]
                {
                new ValidationFailure(nameof(CreateAnimalsAndEnclosuresRequest), message)
            });
            }

            await _enclosureAssignmentService.AssignAllAnimalsToEnclosuresAsync(carnivores, herbivores, enclosureList.ToList());

            var filledEnclosures = await _enclosureService.GetAllAsync();
            var filledEnclosuresResponse = filledEnclosures.ToEnclosuresResponse();

            await SendOkAsync(filledEnclosuresResponse, ct);
        }
    }
}
