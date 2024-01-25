using FastEndpoints;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using ZooAnimalManagement.API.Contracts.Requests;
using ZooAnimalManagement.API.Contracts.Responses;
using ZooAnimalManagement.API.Domain;
using ZooAnimalManagement.API.Mapping;
using ZooAnimalManagement.API.Services;

namespace ZooAnimalManagement.API.Endpoints.AnimalsEndpoints
{
    [HttpPost("upload animals, enclosures and accommodate"), AllowAnonymous]
    public class CreateAndTransferAnimalsToTheirEnclosuresEndpoint : Endpoint<CreateAnimalsAndEnclosuresRequest, GetAllEnclosuresResponse>
    {
        private readonly IAnimalService _animalService;
        private readonly IEnclosureService _enclosureService;
        private readonly IEnclosureAssignmentService _enclosureAssignmentService;

        public CreateAndTransferAnimalsToTheirEnclosuresEndpoint(IAnimalService animalService, IEnclosureService enclosureService, IEnclosureAssignmentService enclosureAssignmentService)
        {
            _animalService = animalService;
            _enclosureService = enclosureService;
            _enclosureAssignmentService = enclosureAssignmentService;
        }

        public override async Task HandleAsync(CreateAnimalsAndEnclosuresRequest request, CancellationToken ct)
        {
            if (request.Animals.Any(animal => animal == null || string.IsNullOrWhiteSpace(animal.Species))) // leaving others unprotected until next code push 
            {
                var message = $"Invalid data found in the 'animals' list";
                throw new ValidationException(message, new[]
                {
            new ValidationFailure(nameof(CreateAnimalsAndEnclosuresRequest.Animals), message)
        });
            }

            if (request.Enclosures.Any(enclosure => enclosure == null || string.IsNullOrWhiteSpace(enclosure.Name))) // leaving others unprotected until next code push 
            {
                var message = $"Invalid data found in the 'enclosures' list";
                throw new ValidationException(message, new[]
                {
            new ValidationFailure(nameof(CreateAnimalsAndEnclosuresRequest.Enclosures), message)
        });
            }

            if (request.Animals != null && request.Animals.Any())
            foreach (var animalDto in request.Animals)
            {
                var animal = animalDto.ToAnimal();

                await _animalService.CreateAsync(animal);
            }

            if (request.Enclosures != null && request.Enclosures.Any())
            foreach (var enclosureDto in request.Enclosures)
            {
                var enclosure = enclosureDto.ToEnclosure();
                await _enclosureService.CreateAsync(enclosure);
            }

            IEnumerable<Animal> animalList = new List<Animal>();
            animalList = await _animalService.GetAllAsync();

            IEnumerable<Enclosure> enclosureList = new List<Enclosure>();
            enclosureList = await _enclosureService.GetAllAsync();

            if ((animalList != null && animalList.Any()) && (enclosureList != null && enclosureList.Any()))
            {
                IEnumerable<Enclosure> animalAccommodatedList = await _enclosureAssignmentService.AssignAllAnimalsToEnclosuresAsync(animalList.ToList(), enclosureList.ToList());
                var enclosureResponse = animalAccommodatedList.ToEnclosuresResponse();
                await SendOkAsync(enclosureResponse, ct);
            }
            else
            {
                var message = $"There is not enough data in the database to process animal tranfer. Number of animal species: {animalList.Count()}, enclosures: {enclosureList.Count()}";
                throw new ValidationException(message, new[]
                {
                new ValidationFailure(nameof(CreateAnimalsAndEnclosuresRequest), message)
            });
            }
        }
    }
}
