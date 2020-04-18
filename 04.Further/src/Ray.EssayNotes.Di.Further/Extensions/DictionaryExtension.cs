using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ray.EssayNotes.Di.Further.Extensions
{
    public static class DictionaryExtension
    {
        public static Dictionary<TKey, string> ToShowDictionary<TKey, TElement>(this IEnumerable<KeyValuePair<TKey, TElement>> source)
        {
            return source.ToDictionary(x => x.Key,
                x => x.Value != null
                    ? $"{x.Value.GetType()}({x.Value.GetHashCode()})"
                    : null);
        }
    }
}
