namespace Demo
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using CSharpSnippets.Decorators.Dictionary;
	using CSharpSnippets.Extensions.Collections;
	using CSharpSnippets.Extensions.RandomExt;
	
	public static class TakeNDemo
	{
		public static void ByCategoryTakeFirstN<TObj,TCategory>( this IDictionary<TObj,TCategory> dict, int n,
		                                                        out IDictionary<TCategory,ISet<TObj>> choices, out IDictionary<TCategory,int> counts )
		{
			counts = new DefaultDictDecorator<TCategory,int> (new Dictionary<TCategory,int> (), () => 0);
			choices = new DefaultDictDecorator<TCategory,ISet<TObj>> (new Dictionary<TCategory,ISet<TObj>> (), () => new HashSet<TObj> ());

			foreach (var kvp in dict) {
				if (counts [kvp.Value]++ < n) {
					Console.WriteLine ("adding {0}th obj to cat {1}; new count {2}", kvp.Key, kvp.Value, counts [kvp.Value]);
					choices [kvp.Value].Add (kvp.Key);
				} else {
					Console.WriteLine ("{0}th obj discarded; count {1} for cat {2} is sufficient", kvp.Key, counts [kvp.Value], kvp.Value);
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
			botToZoneMap.ByCategoryTakeFirstN (10, out choices, out counts );

			Console.WriteLine ("zones to counts:");
			foreach (var kvp in counts) {
				Console.WriteLine ("{0} => {1}", kvp.Key, kvp.Value );
			}

			Console.WriteLine ("zones to choices:");
			foreach (var kvp in choices) {
				Console.WriteLine ("{0} => {1}", kvp.Key, kvp.Value.GetString() );
			}
		}
	}
}

