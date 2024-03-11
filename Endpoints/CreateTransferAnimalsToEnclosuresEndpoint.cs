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
    [HttpPost("Upload animals, enclosures and transfer to accommodate"), AllowAnonymous]
    public class CreateTransferAnimalsToEnclosuresEndpoint : Endpoint<CreateAnimalsAndEnclosuresRequest, GetAllEnclosuresResponse>
    {
        private readonly IAnimalService _animalService;
        private readonly IEnclosureService _enclosureService;
        private readonly IEnclosureAssignmentService _enclosureAssignmentService;

        public CreateTransferAnimalsToEnclosuresEndpoint(IAnimalService animalService, IEnclosureService enclosureService, IEnclosureAssignmentService enclosureAssignmentService)
        {
            _animalService = animalService;
            _enclosureService = enclosureService;
            _enclosureAssignmentService = enclosureAssignmentService;
        }

        public override async Task HandleAsync(CreateAnimalsAndEnclosuresRequest request, CancellationToken ct)
        {
            if (!(request.Animals != null && request.Animals.Any()) || !(request.Enclosures != null && request.Enclosures.Any()))
            {
                var message = $"Both, animal and enclosure lists, must be provided";
                throw new ValidationException(message, new[]
                {
            new ValidationFailure(nameof(CreateAnimalsAndEnclosuresRequest.Animals), message)
        });
            }

            var carnivores = request.Animals
                .Select(animal => animal.ToAnimal())
                .Where(animal => animal.Food == Food.From("Carnivore"))
                .ToList();

            var herbivores = request.Animals
                .Select(animal => animal.ToAnimal())
                .Where(animal => animal.Food == Food.From("Herbivore"))
                .ToList();

            var enclosures = request.Enclosures
                .Select(enclosure => enclosure.ToEnclosure())
                .ToList();

            // due to fact, that Carnivores cannot be accommodated with herbivores, and 2 different species of Carnivores can be in one enclosure
            bool hasExcessCarnivores = carnivores.Count > request.Enclosures.Count * 2;
            bool hasMaxCarnivoresWithHerbivores = carnivores.Count == request.Enclosures.Count * 2 && herbivores.Count > 0;
            bool hasNearMaxCarnivoresWithHerbivores = carnivores.Count + 1 == request.Enclosures.Count * 2 && herbivores.Count > 0;

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

            foreach (var carnivore in carnivores)
            {
                await _animalService.CreateAsync(carnivore);
            }

            foreach (var herbivore in herbivores)
            {
                await _animalService.CreateAsync(herbivore);
            }

            foreach (var enclosure in enclosures)
            {
                await _enclosureService.CreateAsync(enclosure);
            }

            await _enclosureAssignmentService.AssignAllAnimalsToEnclosuresAsync(carnivores, herbivores, enclosures);

            var filledEnclosures = await _enclosureService.GetAllAsync();
            var filledEnclosuresResponse = filledEnclosures.ToEnclosuresResponse();

            await SendOkAsync(filledEnclosuresResponse, ct);
        }
    }
}
