using ValueOf;

namespace ZooAnimalManagement.API.Domain.Common.Animal
{
    public class EnclosureId : ValueOf<Guid?, EnclosureId>
    {
        protected override void Validate()
        {
            if (Value.HasValue && !IsValidGuidString(Value.Value.ToString()))
            {
                throw new ArgumentException("Enclosure Id must be a valid Guid", nameof(EnclosureId));
            }
        }

        private static bool IsValidGuidString(string value)
        {
            return Guid.TryParse(value, out _);
        }
    }
}
