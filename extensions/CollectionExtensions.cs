using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnippets.Extensions.Collections
{
    public static class CollectionExt
    {
        public static bool AddToSet<TKey, TValue, TSetRef, TSetConstr>(this IDictionary<TKey, TSetRef> dict, TKey key, TValue value)
            where TSetRef : ISet<TValue>
            where TSetConstr : TSetRef, new()
        {
            TSetRef theSet;
            if (!dict.TryGetValue(key, out theSet))
            {
                theSet = dict[key] = new TSetConstr();
            }

            return theSet.Add(value);
        }

        public static void AddToCollection<TKey, TValue, TCollRef, TCollConstr>(this IDictionary<TKey, TCollRef> dict, TKey key, TValue value)
            where TCollRef : ICollection<TValue>
            where TCollConstr : TCollRef, new()
        {
            TCollRef theColl;
            if (!dict.TryGetValue(key, out theColl))
            {
                theColl = dict[key] = new TCollConstr();
            }

            theColl.Add(value);
        }
    }
}
