using Dapper;
using ZooAnimalManagement.API.Contracts.Data;
using ZooAnimalManagement.API.Database;

namespace ZooAnimalManagement.API.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public AnimalRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> CreateAsync(AnimalDto animal)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            var result = await connection.ExecuteAsync(
                @"INSERT INTO Animals (Id, Species, Food, Amount, EnclosureId) 
            VALUES (@Id, @Species, @Food, @Amount, @EnclosureId)",
                animal);
            return result > 0;
        }

        public async Task<AnimalDto?> GetAsync(Guid id)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            return await connection.QuerySingleOrDefaultAsync<AnimalDto>(
                "SELECT * FROM Animals WHERE Id = @Id LIMIT 1", new { Id = id.ToString() });
        }

        public async Task<IEnumerable<AnimalDto>> GetAllAsync()
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            return await connection.QueryAsync<AnimalDto>("SELECT * FROM Animals");
        }

        public async Task<bool> UpdateAsync(AnimalDto animal)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            var result = await connection.ExecuteAsync(
                @"UPDATE Animals SET Species = @Species, Food = @Food, Amount = @Amount, 
                 EnclosureId = @EnclosureId WHERE Id = @Id",
                animal);
            return result > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            var result = await connection.ExecuteAsync(@"DELETE FROM Animals WHERE Id = @Id",
                new { Id = id.ToString() });
            return result > 0;
        }
    }
}
