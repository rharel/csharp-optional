using System;

namespace rharel.Functional
{
    /// <summary>
    /// Instantiates optional value types.
    /// </summary>
    public static class Option
    {
        /// <summary>
        /// Creates a new option containing the specified value.
        /// </summary>
        /// <typeparam name="T">The type of the optional value.</typeparam>
        /// <param name="value">The value to contain.</param>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="value"/> is null.
        /// </exception>
        public static Optional<T> Some<T>(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            return new Optional<T>(value);
        }
        /// <summary>
        /// Gets the option containing no value.
        /// </summary>
        /// <typeparam name="T">The type of the optional value.</typeparam>
        public static Optional<T> None<T>()
        {
            return Optional<T>.None;
        }
    }
}
