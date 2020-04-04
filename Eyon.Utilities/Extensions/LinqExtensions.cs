using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Utilities.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey> ( this IEnumerable<TSource> source, Func<TSource, TKey> keySelector )
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach ( TSource element in source )
            {
                if ( seenKeys.Add(keySelector(element)) )
                {
                    yield return element;
                }
            }
        }

        public static IEnumerable<T> GetImagesUrl<T>(this IEnumerable<T> list, Func<T, string> funcToGetUrl )
        {
            foreach ( var item in list )
            {
                funcToGetUrl(item);
                yield return item;
            }
        }
    }
}
