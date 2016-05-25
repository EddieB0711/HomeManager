using System;
using System.Collections.Generic;

namespace HomeManager.Infrastructure.Extensions
{
    public static class ObjectTransformationExtensions
    {
        public static IEnumerable<U> Cast<T, U>(this IEnumerable<T> collection, Func<T, U> transform)
        {
            foreach (var item in collection)
                yield return transform(item);
        }
    }
}