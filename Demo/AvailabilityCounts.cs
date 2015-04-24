namespace Demo
{
    using System.Linq;
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using CSharpSnippets.Decorators.Dictionary;
	using CSharpSnippets.Extensions.Collections;
    using CSharpSnippets.Extensions.Counting;
	using CSharpSnippets.Extensions.RandomExt;
	
	public static class TakeNDemo
	{
        public static void ByCategoryTakeFirstN_Raw<TObj, TCategory>(this IDictionary<TObj, TCategory> dict, int n,
            out IDictionary<TCategory, int> counts,
            out IDictionary<TCategory, ISet<TObj>> choices)
        {
            counts = new Dictionary<TCategory, int>();
            choices = new Dictionary<TCategory, ISet<TObj>>();

            foreach (var kvp in dict)
            {
                TObj item = kvp.Key;
                TCategory key = kvp.Value;

                if (!counts.ContainsKey(key))
                {
                    counts.Add(key, 1);
                }
                else
                {
                    counts[key]++;
                }

                if (counts[key] > n)
                {
                    continue;
                }

                if (!choices.ContainsKey(key))
                {
                    choices.Add(key, new HashSet<TObj> { item });
                }
                else
                {
                    choices[key].Add(item);
                }
            }
        }

		public static void ByCategoryTakeFirstN_Decorated<TObj,TCategory>( this IDictionary<TObj,TCategory> dict, int n,
            out IDictionary<TCategory,int> counts, out IDictionary<TCategory,ISet<TObj>> choices )
		{
			counts = new DefaultDictDecorator<TCategory,int> (new Dictionary<TCategory,int> (), () => 0);
			choices = new DefaultDictDecorator<TCategory,ISet<TObj>> (new Dictionary<TCategory,ISet<TObj>> (), () => new HashSet<TObj> ());

			foreach (var kvp in dict) {
                TObj item = kvp.Key;
                TCategory key = kvp.Value;

				if (counts [key]++ < n)
                {
					choices [key].Add (item);
				}
			}
		}

        public static void ByCategoryTakeFirstN_Extended<TObj, TCategory>(this IDictionary<TObj, TCategory> dict, int n,
            out IDictionary<TCategory, int> counts, out IDictionary<TCategory, ISet<TObj>> choices)
        {
            counts = new Dictionary<TCategory, int>();
            choices = new Dictionary<TCategory, ISet<TObj>>();

            foreach (var kvp in dict)
            {
                TObj item = kvp.Key;
                TCategory key = kvp.Value;

                if (counts.PostPlusPlusAt(key) < n )
                {
                    choices.GetSetAt<TCategory,TObj,ISet<TObj>,HashSet<TObj>>(key).Add(item);
                }
            }
        }




		public static void Run()
		{
			Random rng = new Random (0);

			var botToZoneMap = new Dictionary<string,int> ();
			for (int k=0; k<100; k++) {
				botToZoneMap.Add (string.Format ("bot{0}", k), rng.Next (10));
			}

			IDictionary<int,int> counts;
			IDictionary<int,ISet<string>> choices;
            botToZoneMap.ByCategoryTakeFirstN_Extended(10, out counts, out choices);

			Console.WriteLine ("zones to counts:");
			foreach (var kvp in counts) {
				Console.WriteLine ("{0} => {1}", kvp.Key, kvp.Value );
			}

			Console.WriteLine ("zones to choices:");
			foreach (var kvp in choices) {
				Console.WriteLine ("{0} => {1}", kvp.Key,
                    kvp.Value
                        .Select( bot => string.Format("{0}@zone{1}", bot, botToZoneMap[bot]) )
                        .GetString()
                );
			}
		}
	}
}

