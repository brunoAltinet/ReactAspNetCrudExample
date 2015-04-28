using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Altinet.Extensions.GenericEnumerable
{
    public static class GenericEnumerableExtensions
    {
        public static void Fill<T>(this IList<T> list, T value)
        {
            if (list.Contains(value)) return;
            list.Add(value);
        }

        /// <summary>
        /// Removes all of the items that match the provided condition
        /// </summary>
        /// <typeparam name="T">The type of the items in the list</typeparam>
        /// <param name="list">The list to modify</param>
        /// <param name="whereEvaluator">The test to determine if an item should be removed</param>
        public static void RemoveAll<T>(this IList<T> list, Func<T, bool> whereEvaluator)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (whereEvaluator(list[i])) list.RemoveAt(i);
            }
        }

        /// <summary>
        /// Finds the index in the collection where the predicate evaluates to true.
        /// 
        /// Returns -1 if no matching item found
        /// </summary>
        /// <typeparam name="TSource">Type of collection</typeparam>
        /// <param name="source">Source collection</param>
        /// <param name="predicate">Function to evaluate</param>
        /// <returns>Index where predicate is true, or -1 if not found.</returns>
        public static int IndexWhere<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            var enumerator = source.GetEnumerator();
            int index = 0;
            while (enumerator.MoveNext())
            {
                TSource obj = enumerator.Current;
                if (predicate(obj))
                    return index;
                index++;
            }
            return -1;
        }

        /// <summary>
        /// Finds the index in the collection where the predicate evaluates to true.
        /// 
        /// Returns -1 if no matching item found
        /// </summary>
        /// <typeparam name="TSource">Type of collection</typeparam>
        /// <param name="source">Source collection</param>
        /// <param name="predicate">Function to evaluate</param>
        /// <returns>Index where predicate is true, or -1 if not found.</returns>
        public static int IndexWhere<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            var enumerator = source.GetEnumerator();
            int index = 0;
            while (enumerator.MoveNext())
            {
                TSource obj = enumerator.Current;
                if (predicate(obj, index))
                    return index;
                index++;
            }
            return -1;
        }

        /// <summary>
        /// Concatenates a string between each item in a list of strings
        /// </summary>
        /// <param name="values">The array of strings to join</param>
        /// <param name="separator">The value to concatenate between items</param>
        /// <returns></returns>
        public static string Join(this string[] values, string separator)
        {
            return String.Join(separator, values);
        }

        /// <summary>
        /// Concatenates a string between each item in a sequence of strings
        /// </summary>
        /// <param name="values"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string Join(this IEnumerable<string> values, string separator)
        {
            return Join(values.ToArray(), separator);
        }

        /// <summary>
        /// Performs an action with a counter for each item in a sequence and provides
        /// </summary>
        /// <typeparam name="T">The type of the items in the sequence</typeparam>
        /// <param name="values">The sequence to iterate</param>
        /// <param name="eachAction">The action to performa on each item</param>
        /// <returns></returns>
        public static IEnumerable<T> Each<T>(this IEnumerable<T> values, Action<T, int> eachAction)
        {
            int index = 0;
            foreach (T item in values)
            {
                eachAction(item, index++);
            }

            return values;
        }

        [DebuggerStepThrough]
        public static IEnumerable<T> Each<T>(this IEnumerable<T> values, Action<T> eachAction)
        {
            foreach (T item in values)
            {
                eachAction(item);
            }

            return values;
        }

        [DebuggerStepThrough]
        public static IEnumerable Each(this IEnumerable values, Action<object> eachAction)
        {
            foreach (object item in values)
            {
                eachAction(item);
            }

            return values;
        }

        public static TReturn FirstValue<TItem, TReturn>(this IEnumerable<TItem> enumerable, Func<TItem, TReturn> func)
            where TReturn : class
        {
            return enumerable.Select(func).FirstOrDefault(@object => @object != null);
        }

        /// <summary>
        /// Appends a sequence of items to an existing list
        /// </summary>
        /// <typeparam name="T">The type of the items in the list</typeparam>
        /// <param name="list">The list to modify</param>
        /// <param name="items">The sequence of items to add to the list</param>
        /// <returns></returns>
        public static IList<T> AddRange<T>(this IList<T> list, IEnumerable<T> items)
        {
            items.Each(list.Add);
            return list;
        }

        public static TReturn FirstOfType<TReturn>(this IEnumerable enumerable)
        {
            return enumerable == null ? default(TReturn) : enumerable.OfType<TReturn>().FirstOrDefault();
        }
    }
}