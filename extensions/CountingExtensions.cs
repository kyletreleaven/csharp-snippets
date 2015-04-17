using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnippets.Extensions.CountingExtensions
{
    public static class CountingExtensions
    {
        public static void IncrementAt<T>(this IDictionary<T, int> dict, T key)
        {
            if (!dict.ContainsKey(key))
            {
                dict[key] = 0;
            }
            dict[key]++;
        }

        public static int PrePlusPlusAt<T>(this IDictionary<T, int> dict, T key)
        {
            if (!dict.ContainsKey(key))
            {
                dict[key] = 0;
            }
            return dict[key]++;
        }

        public static int PostPlusPlusAt<T>(this IDictionary<T, int> dict, T key)
        {
            if (!dict.ContainsKey(key))
            {
                dict[key] = 0;
            }
            return ++dict[key];
        }


        public static int CountsPerWeightTotalCount(this IDictionary<int, int> dict)
        {
            int total = 0;
            foreach (var kvp in dict)
            {
                total += kvp.Value * kvp.Key;
            }

            return total;
        }

        public static bool SameCounts<T>(this IDictionary<T, int> countDict, IDictionary<T, int> otherCounts)
        {
            ISet<T> keys = new HashSet<T>(countDict.Keys);
            if (!keys.SetEquals(otherCounts.Keys))
            {
                return false;
            }

            foreach (var kvp in countDict)
            {
                if (otherCounts[kvp.Key] != kvp.Value)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
