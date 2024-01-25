using FluentValidation;
using FluentValidation.Results;
using System.Text.RegularExpressions;
using ValueOf;

namespace ZooAnimalManagement.API.Domain.Common.Enclosure
{
    public class Location : ValueOf<string, Location>
    {
        private static readonly Regex LocationRegex =
            new("^[a-zA-Z]{0,6}$", RegexOptions.Compiled); // this regex regular expression ensures, that Location can contain only consecutive letters, not more than 7 characters long.

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
