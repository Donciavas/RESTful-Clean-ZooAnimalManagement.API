﻿using FastEndpoints;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using ZooAnimalManagement.API.Contracts.Requests;
using ZooAnimalManagement.API.Contracts.Responses;
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
                new ValidationFailure(nameof(CreateAnimalsAndEnclosuresRequest), message)
            });
            }
        }
    }
}