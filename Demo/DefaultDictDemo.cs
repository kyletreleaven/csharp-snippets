namespace Demo
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using CSharpSnippets.Decorators.Dictionary;
	using CSharpSnippets.Extensions.Collections;
	using CSharpSnippets.Extensions.RandomExt;

	public static class EnumerableExt
	{
		public static string GetString<T>( this IEnumerable<T> myEnum )
		{
			return string.Join(", ", myEnum );
		}
	}

	public static class DefaultDictDemo
	{
		public static void Run()
		{
			var sw = new Stopwatch();

			int numString = 10000, maxLen = 10;

			IEnumerable<string> myStrings;
			// myStrings = new string [] { "A", "AB", "BB", "A", "A", "AB" };

			var haveLenDict = new DefaultDictDecorator<int, ICollection<string>>(new Dictionary<int, ICollection<string>>(), () => new HashSet<string>());
			var occuranceDict = new DefaultDictDecorator<string,int> (new Dictionary<string,int> (), () => 0 );

			myStrings = new Random(0).GetStrings(numString, maxLen);

			sw.Restart();
			foreach (var str in myStrings) {
				//occuranceDict [str]++;
				haveLenDict [str.Length].Add (str);
			}
			sw.Stop();
			Console.WriteLine("{0} ticks to completion, defaultdecorated", sw.ElapsedTicks);


			var haveLenDictManual = new Dictionary<int, ICollection<string>>();
			myStrings = new Random(0).GetStrings(numString, maxLen);

			sw.Restart();
			foreach(var str in myStrings)
			{
				ICollection<string> stringsOfLen;
				if (!haveLenDictManual.TryGetValue(str.Length, out stringsOfLen))
				{
					stringsOfLen = haveLenDictManual[str.Length] = new HashSet<string>();
				}

				stringsOfLen.Add(str);
			}
			sw.Stop();
			Console.WriteLine("{0} ticks to completion, manual", sw.ElapsedTicks);

			#if false
			Console.WriteLine ("occurences per distinct word:");
			foreach (var kvp in occuranceDict) {
				Console.WriteLine ("{0} => {1}", kvp.Key, kvp.Value);
			}
			#endif

			var haveLenDictExt = new Dictionary<int, ICollection<string>>();
			myStrings = new Random(0).GetStrings(numString, maxLen);

			sw.Restart();
			foreach (var str in myStrings)
			{
				haveLenDictExt.AddToCollection<int,string,ICollection<string>,HashSet<string>>(str.Length, str);
				//haveLenDictExt.AddToCollection<int, string, HashSet<string>>(str.Length, str);
			}
			sw.Stop();
			Console.WriteLine("{0} ticks to completion, using extensions", sw.ElapsedTicks);


			Console.ReadLine();

			Console.WriteLine ("list of distinct words, by length:");
			foreach (var kvp in haveLenDict) {
				Console.WriteLine ("{0} => {1}", kvp.Key, kvp.Value.GetString() );
			}
		}
	}
}

