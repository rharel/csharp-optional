using System;
using System.Collections.Generic;

namespace rharel.Functional
{
    /// <summary>
    /// Implemented by <see cref="Optional{T}"/> for equality checks.
    /// </summary>
    internal interface Optional
    {
        /// <summary>
        /// Indicates whether this option contains some value.
        /// </summary>
        /// <returns>True iff this option contains some value.</returns>
        bool IsSome { get; }
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
    public struct Optional<T>: Optional
    {
        /// <summary>
        /// The singleton none instance.
        /// </summary>
        internal static readonly Optional<T> None = new Optional<T>();

        /// <summary>
        /// Creates a new option.
        /// </summary>
        /// <param name="value">The value to contain.</param>
        /// <remarks>
        /// If <paramref name="value"/> is null, the option is equivalent to
        /// 
        /// </remarks>
        internal Optional(T value)
        {
            _value = value;
            IsSome = true;
        }

        /// <summary>
        /// Indicates whether this option contains some value.
        /// </summary>
        /// <returns>True iff this option contains some value.</returns>
        public bool IsSome { get; }
        /// <summary>
        /// Indicates whether this option is void.
        /// </summary>
        /// <returns>True iff this option is void.</returns>
        public bool IsNone => !IsSome;

        /// <summary>
        /// Casts this option's value (if any) to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to cast to.</typeparam>
        /// <returns>
        /// A new option containing the cast value of this one.
        /// </returns>
        public Optional<TResult> Cast<TResult>()
        {
            if (IsNone) { return Optional<TResult>.None; }
            else
            {
                return new Optional<TResult>((TResult)(object)_value);
            }
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
            return IsSome ? 
                   EqualityComparer<T>.Default.Equals(query, _value) : 
                   false;
        }
        /// <summary>
        /// Determines whether this option contains the specified value.
        /// </summary>
        /// <param name="query">The value to check against.</param>
        /// <returns>
        /// True iff this option contains <paramref name="query"/>.
        /// </returns>
        bool Optional.Contains(object query)
        {
            if (query is T value) { return Contains(value); }
            else { return false; }
        }

        /// <summary>
        /// Invokes the specified action if this option contains some value.
        /// 
        /// If this option does contain some value, it is passed to the 
        /// specified action as an argument.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        /// <returns>
        /// True iff this option contains some value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="action"/> is null.
        /// </exception>
        public bool ForSome(Action<T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            if (IsSome) { action.Invoke(_value); return true; }
            else { return false; }
        }
        /// <summary>
        /// Invokes one of two specified actions based on this option's 
        /// variant.
        /// 
        /// If this option does contain some value, it is passed to the 
        /// specified action as an argument.
        /// </summary>
        /// <param name="some_handler">
        /// The action to invoke when this option contains some value.
        /// </param>
        /// <param name="none_handler">
        /// The action to invoke when this options is void.
        /// </param>
        /// <returns>
        /// True iff <paramref name="option"/> contains some value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// When either <paramref name="some_handler"/> or 
        /// <paramref name="none_handler"/> is null.
        /// </exception>
        public bool ForSomeOrElse(Action<T> some_handler, Action none_handler)
        {
            if (some_handler == null)
            {
                throw new ArgumentNullException(nameof(some_handler));
            }
            if (none_handler == null)
            {
                throw new ArgumentNullException(nameof(none_handler));
            }
            if (IsSome) { some_handler.Invoke(_value); return true; }
            else { none_handler.Invoke(); return false; }
        }

        /// <summary>
        /// Maps the value of this option using the specified expression. If 
        /// this option is void, returns a default value instead.
        /// 
        /// If this option does contain some value, it is passed to the 
        /// specified expression as an argument.
        /// </summary>
        /// <typeparam name="TResult">
        /// The type of the expression's returned result.
        /// </typeparam>
        /// <param name="expression">The expression to evaluate.</param>
        /// <param name="default_value">
        /// The return value to default to if this option is void.
        /// </param>
        /// <returns>
        /// The returned value of <paramref name="expression"/> if this option
        /// contains some value; otherwise <paramref name="default_value"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="expression"/> is null.
        /// </exception>
        public TResult MapSomeOr<TResult>(
            Func<T, TResult> expression,
            TResult default_value)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            return IsSome? expression.Invoke(_value) : default_value;
        }
        /// <summary>
        /// Maps the value of this option using one of two specified 
        /// expressions, based on this option's variant.
        /// 
        /// If this option does contain some value, it is passed to the 
        /// specified expression as an argument.
        /// </summary>
        /// <typeparam name="TResult">
        /// The type of the expressions' returned result.
        /// </typeparam>
        /// <param name="handle_some">
        /// The expression to evaluate when this option contains some value.
        /// </param>
        /// <param name="handle_none">
        /// The expression to evaluate when this option is void.
        /// </param>
        /// <returns>
        /// The returned value of <paramref name="handle_some"/> if this option
        /// contains some value; otherwise the returned value of 
        /// <paramref name="handle_none"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="handle_some"/> is null.
        /// When <paramref name="handle_none"/> is null.
        /// </exception>
        public TResult MapSomeOrElse<TResult>(
            Func<T, TResult> handle_some,
            Func<TResult> handle_none)
        {
            if (handle_some == null)
            {
                throw new ArgumentNullException(nameof(handle_some));
            }
            if (handle_none == null)
            {
                throw new ArgumentNullException(nameof(handle_none));
            }
            return IsSome ? handle_some.Invoke(_value) : handle_none.Invoke();
        }

        /// <summary>
        /// Unwraps and returns the value of this option if it exists. 
        /// </summary>
        /// <returns>This option's value.</returns>
        /// <exception cref="InvalidOperationException">
        /// When this is a void option.
        /// </exception>
        public T Unwrap()
        {
            if (IsNone)
            {
                throw new InvalidOperationException(
                    "Attempting to unwrap a void option."
                );
            }
            else { return _value; }
        }
        /// <summary>
        /// Unwraps and returns the value of this option if it exists. 
        /// </summary>
        /// <param name="default_value">
        /// The return value to default to when the option is void.
        /// </param>
        /// <returns>
        /// This option's value if it exists;
        /// otherwise <paramref name="default_value"/>.
        /// </returns>
        public T UnwrapOr(T default_value)
        {
            return IsSome ? _value : default_value; 
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
            if (obj is Optional other)
            {
                if (IsSome) { return other.Contains(_value); }
                else { return !other.IsSome; }
            }
            return false;
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
            return IsSome ? _value.GetHashCode() : 0;
        }

        /// <summary>
        /// Returns a string that represents this instance.
        /// </summary>
        /// <returns>A human-readable string.</returns>
        public override string ToString()
        {
            if (IsNone) { return "None"; }
            else { return $"Some{{ {_value} }}"; }
        }

        private readonly T _value;
    }
}
