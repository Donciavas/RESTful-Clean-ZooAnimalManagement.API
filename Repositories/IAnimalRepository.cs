using ZooAnimalManagement.API.Contracts.Data;

namespace ZooAnimalManagement.API.Repositories
{
    public interface IAnimalRepository
    {
        Task<bool> CreateAsync(AnimalDto animal);

        Task<AnimalDto?> GetAsync(Guid id);

        Task<IEnumerable<AnimalDto>> GetAllAsync();

        Task<bool> UpdateAsync(AnimalDto animal);

        Task<bool> DeleteAsync(Guid id);
    }
}
