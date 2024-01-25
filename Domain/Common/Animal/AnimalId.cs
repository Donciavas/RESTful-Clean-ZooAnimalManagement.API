using ValueOf;

namespace ZooAnimalManagement.API.Domain.Common.Animal
{
    public class AnimalId : ValueOf<Guid, AnimalId>
    {
        protected override void Validate()
        {
            if (Value == Guid.Empty)
            {
                throw new ArgumentException("Animal Id cannot be empty", nameof(AnimalId));
            }
        }
    }
}
