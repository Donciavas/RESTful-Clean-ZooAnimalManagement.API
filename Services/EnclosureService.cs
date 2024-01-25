using FluentValidation;
using FluentValidation.Results;
using ZooAnimalManagement.API.Domain;
using ZooAnimalManagement.API.Mapping;
using ZooAnimalManagement.API.Repositories;

namespace ZooAnimalManagement.API.Services
{
    public class EnclosureService : IEnclosureService
    {
        private readonly IEnclosureRepository _enclosureRepository;
        
        public EnclosureService(IEnclosureRepository enclosureRepository)
        {
            _enclosureRepository = enclosureRepository;
        }

        public async Task<bool> CreateAsync(Enclosure enclosure)
        {
            var existingEnclosure = await _enclosureRepository.GetAsync(enclosure.Id.Value);
            if (existingEnclosure is not null)
            {
                var message = $"A enclosure with id {enclosure.Id} already exists";
                throw new ValidationException(message, new[]
                {
                new ValidationFailure(nameof(Enclosure), message)
            });
            }

            var enclosureDto = enclosure.ToEnclosureDto();
            return await _enclosureRepository.CreateAsync(enclosureDto);
        }

        public async Task<Enclosure?> GetAsync(Guid id)
        {
            var enclosureDto = await _enclosureRepository.GetAsync(id);
            return enclosureDto?.ToEnclosure();
        }

        public async Task<IEnumerable<Enclosure>> GetAllAsync()
        {
            var enclosureDtos = await _enclosureRepository.GetAllAsync();
            return enclosureDtos.Select(x => x.ToEnclosure());
        }

        public async Task<bool> UpdateAsync(Enclosure enclosure)
        {
            var enclosureDto = enclosure.ToEnclosureDto();
            return await _enclosureRepository.UpdateAsync(enclosureDto);
        }

        public async Task<bool> DeleteEnclosureAndObjectsAsync(Guid id)
        {
            return await _enclosureRepository.DeleteEnclosureAndObjectsAsync(id);
        }
    }
}
