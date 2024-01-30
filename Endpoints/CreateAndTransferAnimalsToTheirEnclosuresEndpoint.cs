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
    [HttpPost("upload animals, enclosures and transfer to accommodate"), AllowAnonymous]
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
            if (!(request.Animals != null && request.Animals.Any()) || !(request.Enclosures != null && request.Enclosures.Any())) 
            {
                var message = $"Both, animal and enclosure lists, must be provided";
                throw new ValidationException(message, new[]
                {
            new ValidationFailure(nameof(CreateAnimalsAndEnclosuresRequest.Animals), message)
        });
            }

            foreach (var animalDto in request.Animals)
            {
                var animal = animalDto.ToAnimal();

                await _animalService.CreateAsync(animal);
            }

            foreach (var enclosureDto in request.Enclosures)
            {
                var enclosure = enclosureDto.ToEnclosure();
                await _enclosureService.CreateAsync(enclosure);
            }

            IEnumerable<Animal> animalList = new List<Animal>();
            animalList = await _animalService.GetAllAsync();

            IEnumerable<Enclosure> enclosureList = new List<Enclosure>();
            enclosureList = await _enclosureService.GetAllAsync();

            if (animalList.Any() && enclosureList.Any())
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
