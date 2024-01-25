using ZooAnimalManagement.API.Contracts.Data;

namespace ZooAnimalManagement.API.Repositories
{
    public interface IEnclosureRepository
    {
        Task<bool> CreateAsync(EnclosureDto enclosure);

        Task<EnclosureDto?> GetAsync(Guid? id);

        Task<IEnumerable<EnclosureDto>> GetAllAsync();

        Task<bool> UpdateAsync(EnclosureDto enclosure);

        Task<bool> DeleteEnclosureAndObjectsAsync(Guid id);

    }
}
