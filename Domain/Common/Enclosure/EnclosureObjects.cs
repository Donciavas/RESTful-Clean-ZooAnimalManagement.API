using FluentValidation;
using FluentValidation.Results;
using System.Text.RegularExpressions;
using ValueOf;

namespace ZooAnimalManagement.API.Domain.Common.Enclosure
{
    public class EnclosureObjects : ValueOf<List<string>, EnclosureObjects>
    {
        private const int MinLength = 3;
        private const int MaxLength = 24;

        private static readonly Regex ValidObjectRegex =
            new("^[a-z ,]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        protected override void Validate()
        {
            if (Value == null || !Value.All(IsValidObject) || Value.Any(obj => obj == null))
            {
                var message = "Invalid format for enclosure objects. Use letters, spaces, commas";
                throw new ValidationException(message, new[]
                {
                    new ValidationFailure(nameof(EnclosureObjects), message)
                });
            }

            if (Value.Any(obj => obj.Length < MinLength || obj.Length > MaxLength))
            {
                var message = $"Enclosure object length must be between {MinLength} and {MaxLength} characters long";
                throw new ValidationException(message, new[]
                {
                    new ValidationFailure(nameof(EnclosureObjects), message)
                });
            }
        }

        private static bool IsValidObject(string obj)
        {
            return ValidObjectRegex.IsMatch(obj);
        }
    }
}
