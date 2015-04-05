using System;
using System.Collections.Generic;

namespace defaultdict
{
	public static class EnumerableExt
	{
		public static string GetString<T>( this IEnumerable<T> myEnum )
		{
			return string.Join(", ", myEnum );
		}
	}

	class MainClass
	{
		public static void Main (string[] args)
		{
			var occuranceDict = new DefaultDictDecorator<string,int> (new Dictionary<string,int> (), () => 0 );
			var haveLenDict = new DefaultDictDecorator<int, ICollection<string>> (new Dictionary<int, ICollection<string>> (), () => new HashSet<string> ());

			var myStrings = new string [] { "A", "AB", "BB", "A", "A", "AB" };

			foreach (var str in myStrings) {
				occuranceDict [str]++;
				haveLenDict [str.Length].Add (str);
			}

			Console.WriteLine ("occurences per distinct word:");
			foreach (var kvp in occuranceDict) {
				Console.WriteLine ("{0} => {1}", kvp.Key, kvp.Value);
			}

			Console.WriteLine ("list of distinct words, by length:");
			foreach (var kvp in haveLenDict) {
				Console.WriteLine ("{0} => {1}", kvp.Key, kvp.Value.GetString() );
			}

			//Console.WriteLine ("Hello World!");
		}
	}
}
