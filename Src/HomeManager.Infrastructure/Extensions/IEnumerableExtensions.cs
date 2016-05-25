using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeManager.Infrastructure.Extensions
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<T> Where<T, U>(this IEnumerable<T> sourceCollection, IEnumerable<U> dbCollection, Func<T, Func<U, bool>> curriedPredicate)
        {
            var foundItems = new List<T>();

            if (sourceCollection == null || dbCollection == null) return foundItems;

            foreach (var item in sourceCollection)
            {
                var record = dbCollection.FirstOrDefault(curriedPredicate(item));

                if (record != null) foundItems.Add(item);
            }

            return foundItems;
        }

        public static async Task AsyncForEach<T>(this IEnumerable<T> collection, Func<T, Task> @delegate)
        {
            foreach (var item in collection) await @delegate(item);
        }

        public static async Task AwaitAll(this IEnumerable<Task> collection)
        {
            foreach (var item in collection) await item;
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }

        public static void ForEach(this IEnumerable collection, Action<object> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }
    }
}