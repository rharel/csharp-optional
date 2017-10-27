﻿namespace rharel.Optional
{
    /// <summary>
    /// Represents a void optional value.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the optional value.
    /// </typeparam>
    public struct None<T>: Optional<T>
    {
        /// <summary>
        /// Casts this to a void optional value of the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to cast to.</typeparam>
        /// <returns>
        /// A new void optional value of the specified type.
        /// </returns>
        public Optional<TResult> Cast<TResult>()
        {
            return new None<TResult>();
        }
        /// <summary>
        /// Determines whether this option contains the specified value.
        /// </summary>
        /// <param name="query">The value to check against.</param>
        /// <returns>False.</returns>
        public bool Contains(T query)
        {
            return false;
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
            return obj is None<T>;
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
            return 0;
        }

        /// <summary>
        /// Returns a string that represents this instance.
        /// </summary>
        /// <returns>A human-readable string.</returns>
        public override string ToString()
        {
            return nameof(None<T>);
        }
    }
}