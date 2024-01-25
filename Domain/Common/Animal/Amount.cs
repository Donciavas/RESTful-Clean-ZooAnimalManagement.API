using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace ZooAnimalManagement.API.Domain.Common.Animal
{
    public class Amount : ValueOf<int, Amount>
    {
        protected override void Validate()
        {
            if (Value <= 0)
            {
                const string message = "Amount must be a positive integer number";
                throw new ValidationException(message, new[]
                {
                    new ValidationFailure(nameof(Amount), message)
                });
            }
        }
    }
}
