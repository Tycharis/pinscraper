using System;
using System.Collections.Generic;

namespace ChallongeApi
{
    public static class EnumerableExtensionMethods
    {
        /// <summary>
        /// <paramref name="updater"/> updates the items values.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="input"></param>
        /// <param name="updater">Should be used to update the item.</param>
        /// <returns></returns>
        public static IEnumerable<TSource> Set<TSource>(this IEnumerable<TSource> input, Action<TSource> updater)
        {
            // ReSharper disable once ConvertToUsingDeclaration
            // It warns that the enumerator is never disposed, not sure if it is or isn't, so I'll use the old syntax for now
            using (IEnumerator<TSource> enumerator = input.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    updater(enumerator.Current);

                    yield return enumerator.Current;
                }
            }
        }

        /// <summary>
        /// <paramref name="updater"/> updates the items values.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="input"></param>
        /// <param name="updater">Should be used to update the item. Takes the index of the item as well.</param>
        /// <returns></returns>
        public static IEnumerable<TSource> Set<TSource>(this IEnumerable<TSource> input, Action<TSource, int> updater)
        {
            // ReSharper disable once ConvertToUsingDeclaration
            // It warns that the enumerator is never disposed, not sure if it is or isn't, so I'll use the old syntax for now
            using (IEnumerator<TSource> enumerator = input.GetEnumerator())
            {
                int index = 0;

                while (enumerator.MoveNext())
                {
                    updater(enumerator.Current, index);

                    yield return enumerator.Current;

                    checked
                    {
                        index += 1;
                    }
                }
            }
        }
    }
}
