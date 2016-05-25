using System;

namespace HomeManager.Infrastructure.Extensions
{
    public static class NullGuardExtensions
    {
        public static void NullGuard<T>(this T obj, string message = null, string name = null)
        {
            IsNull(obj, message, name);
        }

        public static void NullGuard(this string obj, string message = null, string name = null)
        {
            if (!string.IsNullOrWhiteSpace(obj)) return;

            IsNull<object>(null, message, name);
        }

        public static void NullGuard<T>(this T? obj, string message = null, string name = null) where T : struct
        {
            if (obj.HasValue) return;

            IsNull<object>(null, message, name);
        }

        private static void IsNull<T>(T obj, string message, string name)
        {
            if (obj != null) return;

            if (message != null)
            {
                if (name != null) throw new ArgumentNullException(message, name);

                throw new ArgumentNullException(message);
            }

            if (name != null) throw new ArgumentNullException(string.Empty, name);

            throw new ArgumentNullException();
        }
    }
}