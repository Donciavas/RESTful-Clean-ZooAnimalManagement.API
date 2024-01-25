using ZooAnimalManagement.API.Domain;

namespace ZooAnimalManagement.API.Services
{
    public interface IAnimalService
    {
        Task<bool> CreateAsync(Animal animal);

        Task<Animal?> GetAsync(Guid id);

        Task<IEnumerable<Animal>> GetAllAsync();

        Task<bool> UpdateAsync(Animal animal);

        Task<bool> DeleteAsync(Guid id);
    }
}
