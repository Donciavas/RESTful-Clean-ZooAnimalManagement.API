using FluentValidation;
using FluentValidation.Results;
using System.Text.RegularExpressions;
using ValueOf;

namespace ZooAnimalManagement.API.Domain.Common.Animal
{
    public class Species : ValueOf<string, Species>
    {
        private static readonly Regex SpeciesRegex =
            new("^[a-zA-Z ,'-]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase); // this  regular expression ^[a-z ,'-]+$ ensures that the entire string consists of one or more letters, spaces, commas, single quotes, or hyphens

        protected override void Validate()
        {
            if (!SpeciesRegex.IsMatch(Value))
            {
                var message = $"{Value} is not a valid species name";
                throw new ValidationException(message, new[]
                {
                new ValidationFailure(nameof(Species), message)
            });
            }
        }
    }
}
