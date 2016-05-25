using System;
using System.Collections.Concurrent;
using System.Linq;

namespace HomeManager.Infrastructure.Extensions
{
    public static class ConcurrentBagExtensions
    {
        public static bool TryGet<T>(this ConcurrentBag<T> bag, T searchForValue, Func<T, bool> predicate, out T foundValue) where T : class
        {
            try
            {
                foundValue = bag.FirstOrDefault(predicate);
                return foundValue != null;
            }
            catch (Exception)
            {
                foundValue = default(T);
                return false;
            }
        }
    }
}