using Dapper;
using System.Data;
using ZooAnimalManagement.API.Contracts.Data;
using ZooAnimalManagement.API.Database;

namespace ZooAnimalManagement.API.Repositories
{
    public class EnclosureRepository : IEnclosureRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public EnclosureRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> CreateAsync(EnclosureDto enclosure)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            using var transaction = connection.BeginTransaction();

            try
            {
                var result = await connection.ExecuteAsync(
                    @"INSERT INTO Enclosures (Id, Name, Size, Location) 
              VALUES (@Id, @Name, @Size, @Location)",
                    enclosure, transaction);

                if (result > 0 && enclosure.Objects != null && enclosure.Objects.Any())
                {
                    var enclosureId = await connection.ExecuteScalarAsync<string>(
                        "SELECT Id FROM Enclosures WHERE Id = @Id", new { enclosure.Id }, transaction);

                    var enclosureObjects = enclosure.Objects
                        .Select(obj => new { EnclosureId = enclosureId, Object = obj });

                    await connection.ExecuteAsync(
                        @"INSERT INTO Objects (EnclosureId, Object) 
                  VALUES (@EnclosureId, @Object)",
                        enclosureObjects, transaction);
                }

                transaction.Commit();
                return result > 0;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<EnclosureDto?> GetAsync(Guid? id)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            var enclosure = await connection.QuerySingleOrDefaultAsync<EnclosureDto>(
                "SELECT * FROM Enclosures WHERE Id = @Id LIMIT 1", new { Id = id.ToString() });

            if (enclosure != null)
            {
                var enclosureObjects = await connection.QueryAsync<string>(
                    "SELECT Object FROM Objects WHERE EnclosureId = @EnclosureId",
                    new { EnclosureId = id.ToString() });

                var animals = await connection.QueryAsync<AnimalDto>(
                    "SELECT Id, Species, Food, Amount, EnclosureId FROM Animals WHERE EnclosureId = @EnclosureId",
                    new { EnclosureId = id.ToString() });

                var updatedEnclosure = new EnclosureDto
                {
                    Id = enclosure.Id,
                    Name = enclosure.Name,
                    Size = enclosure.Size,
                    Location = enclosure.Location,
                    Objects = enclosureObjects.ToList(),
                    Animals = animals.ToList()
                };

                return updatedEnclosure;
            }

            return enclosure;
        }

        public async Task<IEnumerable<EnclosureDto>> GetAllAsync()
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            var enclosures = await connection.QueryAsync<EnclosureDto>("SELECT * FROM Enclosures");

            var tasks = enclosures.Select(async enclosure =>
            {
                var enclosureObjects = await connection.QueryAsync<string>(
                    "SELECT Object FROM Objects WHERE EnclosureId = @EnclosureId",
                    new { EnclosureId = enclosure.Id });

                var animals = await connection.QueryAsync<AnimalDto>(
                    "SELECT * FROM EnclosureAnimals EA JOIN Animals A ON EA.AnimalId = A.Id WHERE EA.EnclosureId = @EnclosureId",
                    new { EnclosureId = enclosure.Id });

                return new EnclosureDto
                {
                    Id = enclosure.Id,
                    Name = enclosure.Name,
                    Size = enclosure.Size,
                    Location = enclosure.Location,
                    Objects = enclosureObjects.ToList(),
                    Animals = animals.ToList()
                };
            });

            return await Task.WhenAll(tasks);
        }

        public async Task<bool> UpdateAsync(EnclosureDto enclosure)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            using var transaction = connection.BeginTransaction();

            try
            {
                var updateResult = await connection.ExecuteAsync(@"
                    UPDATE Enclosures 
                    SET Name = @Name, Size = @Size, Location = @Location 
                    WHERE Id = @Id",
                    new
                    {
                        enclosure.Id,
                        enclosure.Name,
                        enclosure.Size,
                        enclosure.Location
                    },
                    transaction);

                if (updateResult > 0)
                {
                    await connection.ExecuteAsync(
                        "DELETE FROM Objects WHERE EnclosureId = @EnclosureId",
                        new { EnclosureId = enclosure.Id },
                        transaction);

                    if (enclosure.Objects != null && enclosure.Objects.Any())
                    {
                        await connection.ExecuteAsync(
                            @"INSERT INTO Objects (EnclosureId, Object) 
                      VALUES (@EnclosureId, @Object)",
                            enclosure.Objects.Select(obj => new
                            {
                                EnclosureId = enclosure.Id,
                                Object = obj
                            }),
                            transaction);
                    }
                }

                transaction.Commit();
                return updateResult > 0;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<bool> DeleteEnclosureAndObjectsAsync(Guid id)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            using var transaction = connection.BeginTransaction();

            try
            {
                await connection.ExecuteAsync(
                    "DELETE FROM Enclosures WHERE Id = @EnclosureId",
                    new { EnclosureId = id.ToString() },
                    transaction);

                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
