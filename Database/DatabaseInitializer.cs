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
            FOREIGN KEY (EnclosureId) REFERENCES Enclosures(Id) ON DELETE CASCADE
            )");

            await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS EnclosureAnimals (
            EnclosureId CHAR(36) NOT NULL,
            AnimalId CHAR(36) NOT NULL,
            PRIMARY KEY (EnclosureId, AnimalId),
            FOREIGN KEY (EnclosureId) REFERENCES Enclosures(Id) ON DELETE CASCADE,
            FOREIGN KEY (AnimalId) REFERENCES Animals(Id) ON DELETE CASCADE
            )");

            await connection.ExecuteAsync(@"CREATE TRIGGER IF NOT EXISTS UpdateEnclosureIdOnAnimals_Insert
                BEFORE INSERT ON Animals
                FOR EACH ROW
                BEGIN
                 INSERT OR IGNORE INTO EnclosureAnimals (EnclosureId, AnimalId)
                    VALUES (NEW.EnclosureId, NEW.Id);
                END");

            await connection.ExecuteAsync(@"CREATE TRIGGER IF NOT EXISTS UpdateEnclosureIdOnAnimals_Update
                BEFORE UPDATE ON Animals
                FOR EACH ROW
                BEGIN
                    DELETE FROM EnclosureAnimals 
                    WHERE AnimalId = NEW.Id AND NEW.EnclosureId IS NULL;

                    UPDATE EnclosureAnimals 
                    SET EnclosureId = NEW.EnclosureId
                    WHERE AnimalId = NEW.Id;

                    INSERT OR IGNORE INTO EnclosureAnimals (EnclosureId, AnimalId)
                    VALUES (NEW.EnclosureId, NEW.Id);
                END");

            await connection.ExecuteAsync(@"CREATE TRIGGER IF NOT EXISTS UpdateEnclosureIdOnEnclosures
                AFTER UPDATE ON Enclosures
                FOR EACH ROW
                BEGIN
                    UPDATE Animals SET EnclosureId = NEW.Id WHERE EnclosureId = OLD.Id;
                    UPDATE EnclosureAnimals SET EnclosureId = NEW.Id WHERE EnclosureId = OLD.Id;
                END");

            await connection.ExecuteAsync(@"CREATE TRIGGER IF NOT EXISTS DeleteEnclosure
                AFTER DELETE ON Enclosures
                FOR EACH ROW
                BEGIN
                    UPDATE Animals SET EnclosureId = NULL WHERE EnclosureId = OLD.Id;
                    DELETE FROM EnclosureAnimals WHERE EnclosureId = OLD.Id;
                END");

            await connection.ExecuteAsync(@"CREATE TRIGGER IF NOT EXISTS DeleteAnimal
                AFTER DELETE ON Animals
                FOR EACH ROW
                BEGIN
                    DELETE FROM EnclosureAnimals WHERE AnimalId = OLD.Id;
                END");
        }
    }
}
