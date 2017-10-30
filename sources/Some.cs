using System;
using System.Collections.Generic;

namespace rharel.Functional
{
    /// <summary>
    /// Represents a non-void optional value.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the optional value.
    /// </typeparam>
    public struct Some<T>: Optional<T>
    {
        /// <summary>
        /// Creates a new option containing the specified value.
        /// </summary>
        /// <param name="value">The value to contain.</param>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="value"/> is null.
        /// </exception>
        public Some(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            Value = value;
        }

        /// <summary>
        /// Gets the value contained in this option.
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Casts the value of this and wraps it in a new option.
        /// </summary>
        /// <typeparam name="TResult">The type to cast to.</typeparam>
        /// <returns>
        /// A new option containing the cast value of this one.
        /// </returns>
        public Optional<TResult> Cast<TResult>()
        {
            return new Some<TResult>((TResult)(object)Value);
        }
        /// <summary>
        /// Determines whether this option contains the specified value.
        /// </summary>
        /// <param name="query">The value to check against.</param>
        /// <returns>
        /// True iff this option contains <paramref name="query"/>.
        /// </returns>
        public bool Contains(T query)
        {
            return EqualityComparer<T>.Default.Equals(query, Value);
        }

        /// <summary>
        /// Determines whether the specified object is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>
        /// True iff <paramref name="obj"/> is equal to this instance. 
        /// </returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Some<T>)) { return false; }

            var other = (Some<T>)obj;

            return EqualityComparer<T>.Default.Equals(other.Value, Value);
        }
        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing 
        /// algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Returns a string that represents this instance.
        /// </summary>
        /// <returns>A human-readable string.</returns>
        public override string ToString()
        {
            return $"{nameof(Some<T>)}{{ " +
                   $"{nameof(Value)} = {Value} }}";
        }
    }
}
