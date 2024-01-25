using FluentValidation;
using FluentValidation.Results;
using ZooAnimalManagement.API.Domain;
using ZooAnimalManagement.API.Mapping;
using ZooAnimalManagement.API.Repositories;

namespace ZooAnimalManagement.API.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _animalRepository;

        public AnimalService(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<bool> CreateAsync(Animal animal)
        {
            var existingAnimal = await _animalRepository.GetAsync(animal.Id.Value);
            if (existingAnimal is not null)
            {
                var message = $"An animal with id {animal.Id} already exists";
                throw new ValidationException(message, new[]
                {
                new ValidationFailure(nameof(Animal), message)
            });
            }

            var animalDto = animal.ToAnimalDto();
            return await _animalRepository.CreateAsync(animalDto);
        }

        public async Task<Animal?> GetAsync(Guid id)
        {
            var animalDto = await _animalRepository.GetAsync(id);
            return animalDto?.ToAnimal();
        }

        public async Task<IEnumerable<Animal>> GetAllAsync()
        {
            var animalDtos = await _animalRepository.GetAllAsync();
            return animalDtos.Select(x => x.ToAnimal());
        }

        public async Task<bool> UpdateAsync(Animal animal)
        {
            var animalDto = animal.ToAnimalDto();
            return await _animalRepository.UpdateAsync(animalDto);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _animalRepository.DeleteAsync(id);
        }
    }
}
