using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Utilities
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
        public static bool ContainsAny( this string str, params string[] values )
        {
            if ( !string.IsNullOrEmpty(str) || values.Length > 0 )
            {
                foreach ( string value in values )
                {
                    if ( str.Contains(value) )
                        return true;
                }
            }

            return false;
        }
    }
}
