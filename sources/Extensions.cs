using System;

namespace rharel.Functional
{
    /// <summary>
    /// Contains extension methods for optional value types.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Invokes the specified action if the specified option contains some 
        /// value.
        /// 
        /// If the option does contain some value, it is passed to the 
        /// specified action as an argument.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the optional value.
        /// </typeparam>
        /// <param name="option">The option to test.</param>
        /// <param name="action">The action to invoke.</param>
        /// <returns>
        /// True iff <paramref name="option"/> contains some value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="action"/> is null.
        /// </exception>
        public static bool ForSome<T>(this Optional<T> option,
                                      Action<T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            switch (option)
            {
                case Some<T> some:
                {
                    action.Invoke(some.Value);
                    return true;
                }
                default:
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Invokes one of two specified actions based on whether the specified
        /// option's variant.
        /// 
        /// If the option does contain some value, it is passed to the 
        /// specified action as an argument.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the optional value.
        /// </typeparam>
        /// <param name="option">The option to test.</param>
        /// <param name="some_handler">
        /// The action to invoke when the option contains some value.
        /// </param>
        /// <param name="none_handler">
        /// The action to invoke when the options is void.
        /// </param>
        /// <returns>
        /// True iff <paramref name="option"/> contains some value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// When either <paramref name="some_handler"/> or 
        /// <paramref name="none_handler"/> is null.
        /// </exception>
        public static bool ForSomeOrElse<T>(this Optional<T> option,
                                            Action<T> some_handler,
                                            Action none_handler)
        {
            if (some_handler == null)
            {
                throw new ArgumentNullException(nameof(some_handler));
            }
            if (none_handler == null)
            {
                throw new ArgumentNullException(nameof(none_handler));
            }
            switch (option)
            {
                case Some<T> some:
                {
                    some_handler.Invoke(some.Value);
                    return true;
                }
                default:
                {
                    none_handler.Invoke();
                    return false;
                }
            }
        }

        /// <summary>
        /// Maps the value of the specified option using the specified 
        /// expression. If the option is void, returns a default value
        /// instead.
        /// 
        /// If the option does contain some value, it is passed to the 
        /// specified expression as an argument.
        /// </summary>
        /// <typeparam name="TValue">
        /// The type of the optional value.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the expression's returned result.
        /// </typeparam>
        /// <param name="option">The option to test.</param>
        /// <param name="expression">The expression to evaluate.</param>
        /// <param name="default_value">
        /// The return value to default to if the option is void.
        /// </param>
        /// <returns>
        /// The returned value of <paramref name="expression"/> if 
        /// <paramref name="option"/> contains some value; 
        /// otherwise <paramref name="default_value"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="expression"/> is null.
        /// </exception>
        public static TResult MapSomeOr<TValue, TResult>(
            this Optional<TValue> option,
            Func<TValue, TResult> expression,
            TResult default_value)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            switch (option)
            {
                case Some<TValue> some:
                {
                    return expression.Invoke(some.Value);
                }
                default:
                {
                    return default_value;
                }
            }
        }
        /// <summary>
        /// Maps the value of the specfied option using one of two specified 
        /// expressions, based on whether the specified option's variant.
        /// 
        /// If the option does contain some value, it is passed to the 
        /// specified expression as an argument.
        /// </summary>
        /// <typeparam name="TValue">
        /// The type of the optional value.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the expressions' returned result.
        /// </typeparam>
        /// <param name="option">The option to test.</param>
        /// <param name="handle_some">
        /// The expression to evaluate when the option contains some value.
        /// </param>
        /// <param name="handle_none">
        /// The expression to evaluate when the option is void.
        /// </param>
        /// <returns>
        /// The returned value of <paramref name="handle_some"/> if 
        /// <paramref name="option"/> contains some value; 
        /// otherwise the returned value of <paramref name="handle_none"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// When either <paramref name="handle_some"/> or 
        /// <paramref name="handle_none"/> is null.
        /// </exception>
        public static TResult MapSomeOrElse<TValue, TResult>(
            this Optional<TValue> option,
            Func<TValue, TResult> handle_some,
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
            switch (option)
            {
                case Some<TValue> some:
                {
                    return handle_some.Invoke(some.Value);
                }
                default:
                {
                    return handle_none.Invoke();
                }
            }
        }
        
        /// <summary>
        /// Unwraps and returns the value of the specified option if it exists; 
        /// throws an exception otherwise.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the optional value.
        /// </typeparam>
        /// <param name="option">The option to unwrap.</param>
        /// <returns>The option's value.</returns>
        /// <exception cref="InvalidOperationException">
        /// When <paramref name="option"/> is a void option.
        /// </exception>
        public static T Unwrap<T>(this Optional<T> option)
        {
            switch (option)
            {
                case Some<T> some:
                {
                    return some.Value;
                }
                default:
                {
                    throw new InvalidOperationException(
                        "Attempting to unwrap a void option."
                    );
                }
            }
        }
        /// <summary>
        /// Unwraps and returns the value of the specified option if it exists; 
        /// returns a specified default value otherwise.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the optional value.
        /// </typeparam>
        /// <param name="option">The option to unwrap.</param>
        /// <param name="default_value">
        /// The return value to default to when the option is void.
        /// </param>
        /// <returns>
        /// <paramref name="option"/>'s value if it exists;
        /// otherwise <paramref name="default_value"/>.
        /// </returns>
        public static T UnwrapOr<T>(this Optional<T> option, T default_value)
        {
            switch (option)
            {
                case Some<T> some:
                {
                    return some.Value;
                }
                default:
                {
                    return default_value;
                }
            }
        }
    }
}
