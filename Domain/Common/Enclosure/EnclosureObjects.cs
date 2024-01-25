using FluentValidation;
using FluentValidation.Results;
using System.Text.RegularExpressions;
using ValueOf;

namespace ZooAnimalManagement.API.Domain.Common.Enclosure
{
    public class EnclosureObjects : ValueOf<List<string>, EnclosureObjects>
    {
        private static readonly Regex ValidObjectRegex =
            new Regex("^[a-zA-Z ,]+$", RegexOptions.Compiled);

        protected override void Validate()
        {
            if (Value == null || !Value.All(IsValidObject))
            {
                var message = "Invalid format for enclosure objects. Use letters, spaces, commas";
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
