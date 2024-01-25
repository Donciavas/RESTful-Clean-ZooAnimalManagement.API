using FluentValidation;
using FluentValidation.Results;
using System.Text.RegularExpressions;
using ValueOf;

namespace ZooAnimalManagement.API.Domain.Common.Enclosure
{
    public class Name : ValueOf<string, Name>
    {
        private static readonly Regex NameRegex =
            new("^[a-zA-Z 0-9,'-]+$", RegexOptions.Compiled); // this regex regular expression ensures, that name can contain letters, digits, spaces, commas, single quotes, or hyphens.

        protected override void Validate()
        {
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
