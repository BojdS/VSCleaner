using System;
using System.Collections.Generic;

namespace VSCleaner.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> ToEnumerableOfOne<T>(this T src)
        {
            if (src == null)
                throw new NullReferenceException("Objet couldn't be null");

            yield return src;
        }
    }
}