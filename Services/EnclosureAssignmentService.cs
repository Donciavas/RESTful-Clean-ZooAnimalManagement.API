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
        public async Task AssignAllAnimalsToEnclosuresAsync(List<Animal> carnivores, List<Animal> herbivores, List<Enclosure> allEnclosures)
        {           
            var sortedCarnivoreEnclosures = allEnclosures.OrderBy(e => e.Size).ToList();
            var sortedHerbivoreEnclosures = allEnclosures.OrderByDescending(e => e.Size).ToList();

            // Case 1: Carnivores equal to double the enclosures and no herbivores present
            if (carnivores.Count == allEnclosures.Count * 2 && herbivores.Count == 0)
            {
                await AssignAnimalsToEnclosuresAsync(carnivores, sortedCarnivoreEnclosures);
                return; // Early return as we've handled all animals in this specific case
            }

            // Case 2: Check for carnivore direct 1:1 assignment to enclosure possibility with an extra enclosures for herbivores. 
            if ((carnivores.Count + 1) <= allEnclosures.Count)
            {
                foreach (var carnivore in carnivores)
                    await TryAssignAnimalToEmptyEnclosureAsync(sortedCarnivoreEnclosures, carnivore);
                await AssignAnimalsToEnclosuresAsync(herbivores, sortedHerbivoreEnclosures);
            }
            // Case 3: Mixed assignment based on enclosure availability. It means 2 different species of meat-eating animals will be grouped together, 
            //herbivores based on enclosure availability.
            else if (allEnclosures.Count > (carnivores.Count / 2)) // with 6 enclosures and 11 carnivores it goes in, but it shouldn't
            {
                await AssignAnimalsToEnclosuresAsync(carnivores, sortedCarnivoreEnclosures);
                await AssignAnimalsToEnclosuresAsync(herbivores, sortedHerbivoreEnclosures);
            }
        }

        private async Task AssignAnimalsToEnclosuresAsync(List<Animal> animals, List<Enclosure> sortedEnclosures)
        {
            foreach (var animal in animals)
            {
                bool suitableEnclosureFound = false;

                suitableEnclosureFound = await TryAssignAnimalToEmptyEnclosureAsync(sortedEnclosures, animal);

                if (!suitableEnclosureFound)
                {
                    suitableEnclosureFound = await TryAssignAnimalToEnclosureAsync(sortedEnclosures, animal, suitableEnclosureFound);
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
            var foundEmpty = false;
            Enclosure? suitableEnclosure = null;

            if (sortedEnclosures.Count(e => e.Animals.Count == 0) > 1 || animal.Food == Food.From("Herbivore"))
            {
                suitableEnclosure = sortedEnclosures.FirstOrDefault(e => e.Animals.Count == 0);
            }

            if (suitableEnclosure == null && animal.Food == Food.From("Herbivore"))
            {
                suitableEnclosure = sortedEnclosures.FirstOrDefault(e =>
                e.Animals.All(a => a.Food == Food.From("Herbivore"))); // if we lack enclosures to accommodate animals more comfortable, then squeeze herbivore into single enclosure
            }

            if (suitableEnclosure != null)
            {
                suitableEnclosure.Animals.Add(animal);
                await UpdateEnclosureIdForAnimalAsync(animal, suitableEnclosure.Id);
                foundEmpty = true;
            }

            return foundEmpty;
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
