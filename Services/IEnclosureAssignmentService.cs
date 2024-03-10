using ZooAnimalManagement.API.Domain;

namespace ZooAnimalManagement.API.Services
{
    public interface IEnclosureAssignmentService
    {
        Task AssignAllAnimalsToEnclosuresAsync(List<Animal> allAnimals, List<Enclosure> allEnclosures);
    }
}