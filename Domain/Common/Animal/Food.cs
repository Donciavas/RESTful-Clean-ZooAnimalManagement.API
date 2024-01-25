using FluentValidation;
using FluentValidation.Results;
using System.Text.RegularExpressions;
using ValueOf;

namespace ZooAnimalManagement.API.Domain.Common.Animal
{
    public class Food : ValueOf<string, Food>
    {
        private static readonly Regex FoodRegex =
            new("^[a-zA-Z ,'-]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase); // this  regular expression ^[a-z ,'-]+$ ensures that the entire string consists of one or more letters, spaces, commas, single quotes, or hyphens

        protected override void Validate()
        {
            if (!FoodRegex.IsMatch(Value))
            {
                var message = $"{Value} is not a valid food name";
                throw new ValidationException(message, new[]
                {
                new ValidationFailure(nameof(Food), message)
            });
            }
        }
    }
}
