using FluentValidation;
using FluentValidation.Results;

namespace ZooAnimalManagement.API.Domain.Common.Enclosure
{
    public class Size : IComparable<Size>
    {
        public static readonly Size Small = new Size(nameof(Small), 0);
        public static readonly Size Medium = new Size(nameof(Medium), 1);
        public static readonly Size Large = new Size(nameof(Large), 2);
        public static readonly Size Huge = new Size(nameof(Huge), 3);

        private static readonly Dictionary<string, Size> SizesByName = new Dictionary<string, Size>(StringComparer.OrdinalIgnoreCase)
        {
            { nameof(Small), Small },
            { nameof(Medium), Medium },
            { nameof(Large), Large },
            { nameof(Huge), Huge }
        };

        public string Value { get; }
        public int Order { get; }

        private Size(string value, int order)
        {
            Value = value;
            Order = order;
        }

        public static Size FromString(string size)
        {
            if (SizesByName.TryGetValue(size, out Size? result))
            {
                return result;
            }

            var message = $"{size} is not a valid size. Valid sizes are: {string.Join(", ", SizesByName.Keys)}";
            throw new ValidationException(message, new[]
            {
                new ValidationFailure(nameof(Size), message)
            });
        }

        public int CompareTo(Size? other)
        {
            return Order.CompareTo(other?.Order);
        }
    }
}
