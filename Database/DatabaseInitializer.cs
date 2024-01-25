using Dapper;

namespace ZooAnimalManagement.API.Database
{
    public class DatabaseInitializer
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public DatabaseInitializer(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task InitializeAsync()
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS Animals (
            Id CHAR(36) PRIMARY KEY, 
            Species TEXT NOT NULL,
            Food TEXT NOT NULL,
            Amount INTEGER NOT NULL,
            EnclosureId CHAR(36),
            FOREIGN KEY (EnclosureId) REFERENCES Enclosures(Id)
            )");

            await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS Enclosures (
            Id CHAR(36) PRIMARY KEY, 
            Name TEXT NOT NULL,
            Size TEXT NOT NULL,
            Location TEXT NOT NULL
            )");

            await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS Objects (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            EnclosureId CHAR(36) NOT NULL,
            Object TEXT NOT NULL,
            FOREIGN KEY (EnclosureId) REFERENCES Enclosures(Id)
            )");

            await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS EnclosureAnimals (
            EnclosureId CHAR(36) NOT NULL,
            AnimalId CHAR(36) NOT NULL,
            PRIMARY KEY (EnclosureId, AnimalId),
            FOREIGN KEY (EnclosureId) REFERENCES Enclosures(Id),
            FOREIGN KEY (AnimalId) REFERENCES Animals(Id)
            )");
        }
    }
}
