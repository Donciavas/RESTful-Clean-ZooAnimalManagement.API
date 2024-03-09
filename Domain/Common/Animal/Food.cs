using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace ZooAnimalManagement.API.Domain.Common.Animal
{
    public class Food : ValueOf<string, Food>
    {
        protected override void Validate()
        {
            var validFoodType = new[] { "Carnivore", "Herbivore" };
            if (!validFoodType.Contains(Value, StringComparer.OrdinalIgnoreCase))
            {
                var message = $"{Value} is not a valid food type. Choose between 'Carnivore' or 'Herbivore'";
                throw new ValidationException(message, new[]
                {
                new ValidationFailure(nameof(Food), message)
            });
            }
        }
    }
}
