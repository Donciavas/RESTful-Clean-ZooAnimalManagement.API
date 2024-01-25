using FluentValidation;
using FluentValidation.Results;

namespace ZooAnimalManagement.API.Domain.Common.Enclosure
{
    public class Size : IComparable<Size>
    {
        public static readonly Size Small = new Size(nameof(Small));
        public static readonly Size Medium = new Size(nameof(Medium));
        public static readonly Size Large = new Size(nameof(Large));
        public static readonly Size Huge = new Size(nameof(Huge));

        private static readonly HashSet<string> ValidSizeValues = new HashSet<string>
    {
        nameof(Small),
        nameof(Medium),
        nameof(Large),
        nameof(Huge)
    };

        public string Value { get; }

        private Size(string value)
        {
            Value = value;
        }

        public static Size FromString(string size)
        {
            if (ValidSizeValues.Contains(size))
            {
                return new Size(size);
            }

            var message = $"{size} is not a valid size. Valid sizes are: {string.Join(", ", ValidSizeValues)}";
            throw new ValidationException(message, new[]
            {
            new ValidationFailure(nameof(Size), message)
        });
        }

        public int CompareTo(Size other)
        {
            return Comparer<string>.Default.Compare(Value, other.Value);
        }
    }
}
