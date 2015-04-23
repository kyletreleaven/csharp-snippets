using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnippets.Extensions.Counting
{
    public static class CountingExtensions
    {
		public static int CountAt<T>(this IDictionary<T, int> dict, T key)
		{
			int count;
			if (dict.TryGetValue (key, out count)) {
				return count;
			}
			return 0;
		}

        public static void IncrementAt<T>(this IDictionary<T, int> dict, T key)
        {
			dict [key] = dict.CountAt (key) + 1;
        }

        public static int PrePlusPlusAt<T>(this IDictionary<T, int> dict, T key)
        {
			int count = dict.CountAt (key);
			dict.IncrementAt (key);
			return count;
        }

        public static int PostPlusPlusAt<T>(this IDictionary<T, int> dict, T key)
        {
			dict.IncrementAt (key);
			return dict [key];
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
			ISet<T> keys = new HashSet<T> (countDict.Keys);
			keys.UnionWith (otherCounts.Keys);

			foreach (var kvp in countDict) {
				if (otherCounts.CountAt (kvp.Key) != kvp.Value) {
					return false;
				}
			}

			return true;
		}
    }
}
