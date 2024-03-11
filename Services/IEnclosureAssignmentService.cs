using ZooAnimalManagement.API.Domain;

namespace ZooAnimalManagement.API.Services
{
    public interface IEnclosureAssignmentService
    {
        Task AssignAllAnimalsToEnclosuresAsync(List<Animal> carnivores, List<Animal> herbivores, List<Enclosure> allEnclosures);
    }
}