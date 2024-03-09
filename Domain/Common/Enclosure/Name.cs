using FluentValidation;
using FluentValidation.Results;
using System.Text.RegularExpressions;
using ValueOf;

namespace ZooAnimalManagement.API.Domain.Common.Enclosure
{
    public class Name : ValueOf<string, Name>
    {
        private const int MinLength = 3;
        private const int MaxLength = 24;

        private static readonly Regex NameRegex =
            new("^[a-zA-Z 0-9,'-]+$", RegexOptions.Compiled); // this regex regular expression ensures, that name can contain letters, digits, spaces, commas, single quotes, or hyphens.

        protected override void Validate()
        {
            if (Value == null)
            {
                var message = "Name value cannot be null";
                throw new ValidationException(message, new[]
                {
                    new ValidationFailure(nameof(Name), message)
                });
            }

            if (Value.Length < MinLength || Value.Length > MaxLength)
            {
                var message = $"Name value must be between {MinLength} and {MaxLength} characters long";
                throw new ValidationException(message, new[]
                {
                    new ValidationFailure(nameof(Name), message)
                });
            }

            if (!NameRegex.IsMatch(Value))
            {
                var message = $"{Value} is not a valid name";
                throw new ValidationException(message, new[]
                {
                    new ValidationFailure(nameof(Name), message)
                });
            }
        }
    }
}
