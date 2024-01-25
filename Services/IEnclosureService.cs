using ZooAnimalManagement.API.Domain;

namespace ZooAnimalManagement.API.Services
{
    public interface IEnclosureService
    {
        Task<bool> CreateAsync(Enclosure enclosure);

        Task<Enclosure?> GetAsync(Guid id);

        Task<IEnumerable<Enclosure>> GetAllAsync();

        Task<bool> UpdateAsync(Enclosure enclosure);

        Task<bool> DeleteEnclosureAndObjectsAsync(Guid id);
    }
}
