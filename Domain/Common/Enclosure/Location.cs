using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace ZooAnimalManagement.API.Domain.Common.Enclosure
{
    public class Location : ValueOf<string, Location>
    {

        protected override void Validate()
        {
            var validLocations = new[] { "Inside", "Outside" };

            if (!validLocations.Contains(Value, StringComparer.OrdinalIgnoreCase))
            {
                var message = $"{Value} is not a valid location. Choose 'Inside' or 'Outside'";
                throw new ValidationException(message, new[]
                {
                    new ValidationFailure(nameof(Location), message)
                });
            }
        }
    }
}
