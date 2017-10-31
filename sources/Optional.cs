namespace rharel.Functional
{
    /// <summary>
    /// Represents an optional value type with two states: one that contains 
    /// some value, and one that does not.
    /// </summary>
    /// <remarks>
    /// <see cref="Optional{T}"/> for the generic version.
    /// </remarks>
    public interface Optional
    {
        /// <summary>
        /// Determines whether this option contains the specified value.
        /// </summary>
        /// <param name="query">The value to check against.</param>
        /// <returns>
        /// True iff this option contains <paramref name="query"/>.
        /// </returns>
        bool Contains(object query);
    }
    /// <summary>
    /// Represents an optional value type with two states: one that contains 
    /// some value, and one that does not.
    /// </summary>
    /// <typeparam name="T">The type of the optional value.</typeparam>
    /// <remarks>
    /// <see cref="Optional"/> for the non-generic version.
    /// </remarks>
    public interface Optional<T>: Optional
    {
        /// <summary>
        /// Casts this option's value (if any) to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to cast to.</typeparam>
        /// <returns>
        /// A new option containing the cast value of this one.
        /// </returns>
        Optional<TResult> Cast<TResult>();
        /// <summary>
        /// Determines whether this option contains the specified value.
        /// </summary>
        /// <param name="query">The value to check against.</param>
        /// <returns>
        /// True iff this option contains <paramref name="query"/>.
        /// </returns>
        bool Contains(T query);
    }
}
