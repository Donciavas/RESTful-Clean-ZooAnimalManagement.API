using FluentValidation;
using FluentValidation.Results;
using ZooAnimalManagement.API.Contracts.Requests;
using ZooAnimalManagement.API.Domain;
using ZooAnimalManagement.API.Domain.Common.Animal;
using ZooAnimalManagement.API.Repositories;

namespace ZooAnimalManagement.API.Services
{
    public class EnclosureAssignmentService : IEnclosureAssignmentService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IEnclosureRepository _enclosureRepository;
        public EnclosureAssignmentService(IAnimalRepository animalRepository, IEnclosureRepository enclosureRepository)
        {
            _animalRepository = animalRepository;
            _enclosureRepository = enclosureRepository;
        }
        public async Task<IEnumerable<Enclosure>> AssignAllAnimalsToEnclosuresAsync(List<Animal> allAnimals, List<Enclosure> allEnclosures)
        {
            var carnivores = allAnimals.Where(animal => animal.Food == Food.From("Carnivore")).ToList();
            var herbivores = allAnimals.Where(animal => animal.Food == Food.From("Herbivore")).ToList();

            await AssignAnimalsToEnclosuresAsync(carnivores, allEnclosures, true);
            await AssignAnimalsToEnclosuresAsync(herbivores, allEnclosures, false);           
            
            return allEnclosures;
        }

        private async Task AssignAnimalsToEnclosuresAsync(List<Animal> animals, List<Enclosure> allEnclosures, bool isCarnivore)
        {
            var sortedEnclosures = isCarnivore
                ? allEnclosures.OrderByDescending(e => e.Size).ToList()
                : allEnclosures.OrderBy(e => e.Size).ToList();

            foreach (var animal in animals)
            {
                bool suitableEnclosureFound = false;

                suitableEnclosureFound = await TryAssignAnimalToEnclosureAsync(sortedEnclosures, animal, suitableEnclosureFound);

                if (!suitableEnclosureFound)
                {
                    suitableEnclosureFound = await TryAssignAnimalToEmptyEnclosureAsync(sortedEnclosures, animal);
                }

                if (!suitableEnclosureFound)
                {
                    var message = $"Animals and enclosures were added to the system, " +
                        $"but unable to find a suitable enclosure to accommodate animals, " +
                        $"starting from the {animal.Species}, {animal.Food} with ID {animal.Id}, " +
                        $"to the end of the list";
                    throw new ValidationException(message, new[]
                    {
                new ValidationFailure(nameof(CreateAnimalsAndEnclosuresRequest), message)
            });
                }
            }
        }

        private async Task<bool> TryAssignAnimalToEnclosureAsync(List<Enclosure> sortedEnclosures, Animal animal, bool suitableEnclosureFound)
        {
            var suitableEnclosure = sortedEnclosures.FirstOrDefault(e =>
                e.Animals.Any(a => a.Species == animal.Species) ||
                (e.Animals.Count < 2 && e.Animals.All(a => a.Food == animal.Food))); // accommodate animals based on rules,
                                                                                     // until there is enough enclosures left after Carnivore
                                                                                     // accommodation, let herbivore live as comfortably as carnivore

            if (suitableEnclosure == null)
            {
                suitableEnclosure = sortedEnclosures.FirstOrDefault(e =>
                e.Animals.All(a => a.Food == Food.From("Herbivore"))); // if we lack enclosures to accommodate animals comfortably, then squeeze herbivore into single enclosure
            }

            if (suitableEnclosure != null)
            {
                suitableEnclosure.Animals.Add(animal);
                await UpdateEnclosureIdForAnimalAsync(animal, suitableEnclosure.Id);
                suitableEnclosureFound = true;
            }

            return suitableEnclosureFound;
        }

        private async Task<bool> TryAssignAnimalToEmptyEnclosureAsync(List<Enclosure> sortedEnclosures, Animal animal)
        {
            var suitableEnclosure = sortedEnclosures.FirstOrDefault(e => e.Animals.Count == 0);

            if (suitableEnclosure != null)
            {
                suitableEnclosure.Animals.Add(animal);
                await UpdateEnclosureIdForAnimalAsync(animal, suitableEnclosure.Id);
            }

            return suitableEnclosure != null;
        }

        private async Task UpdateEnclosureIdForAnimalAsync(Animal animal, EnclosureId enclosureId)
        {
            var existingAnimal = await _animalRepository.GetAsync(animal.Id.Value);

            if (existingAnimal != null)
            {
                existingAnimal.EnclosureId = enclosureId.ToString();
                await _animalRepository.UpdateAsync(existingAnimal);
            }
        }
    }
}
