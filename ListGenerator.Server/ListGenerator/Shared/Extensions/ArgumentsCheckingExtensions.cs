using System;

namespace ListGenerator.Shared.Extensions
{
    public static class ArgumentsCheckingExtensions
    {
        public static void ThrowIfArgumentIsNull(this string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
        }

        public static void ThrowIfArgumentIsNullOrEmpty(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException(nameof(value));
            }
        }
    }
}
