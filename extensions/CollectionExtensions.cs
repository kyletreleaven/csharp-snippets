using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnippets.Extensions.CollectionExtensions
{
    public static class CollectionExt
    {
        public static bool AddToSet<TKey, TValue, TSet>(this IDictionary<TKey, TSet> dict, TKey key, TValue value)
            where TSet : ISet<TValue>, new()
        {
            TSet theSet;
            if (!dict.TryGetValue(key, out theSet))
            {
                theSet = dict[key] = new TSet();
            }

            return theSet.Add(value);
        }

        public static void AddToCollection<TKey, TValue, TCollection>(this IDictionary<TKey, TCollection> dict, TKey key, TValue value)
            where TCollection : ICollection<TValue>, new()
        {
            TCollection theColl;
            if (!dict.TryGetValue(key, out theColl))
            {
                theColl = dict[key] = new TCollection();
            }

            theColl.Add(value);
        }
    }
}
