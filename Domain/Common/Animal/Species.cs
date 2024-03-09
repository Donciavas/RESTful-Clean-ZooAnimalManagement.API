using FluentValidation;
using FluentValidation.Results;
using System.Text.RegularExpressions;
using ValueOf;

namespace ZooAnimalManagement.API.Domain.Common.Animal
{
    public class Species : ValueOf<string, Species>
    {
        private const int MinLength = 3;
        private const int MaxLength = 24;

        private static readonly Regex SpeciesRegex =
            new("^[a-z ,'-]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase); // this  regular expression ^[a-z ,'-]+$ ensures that the entire string consists of one or more letters, spaces, commas, single quotes, or hyphens

        protected override void Validate()
        {
            if (Value == null)
            {
                var message = "Species value cannot be null";
                throw new ValidationException(message, new[]
                {
                    new ValidationFailure(nameof(Species), message)
                });
            }

            if (Value.Length < MinLength || Value.Length > MaxLength)
            {
                var message = $"Species value must be between {MinLength} and {MaxLength} characters long";
                throw new ValidationException(message, new[]
                {
                    new ValidationFailure(nameof(Species), message)
                });
            }

            if (!SpeciesRegex.IsMatch(Value))
            {
                var message = $"{Value} is not a valid species name, use a-z and ', ' -' symbols only";
                throw new ValidationException(message, new[]
                {
                new ValidationFailure(nameof(Species), message)
                });
            }
        }
    }
}
