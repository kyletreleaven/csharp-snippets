using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using CSharpSnippets.Extensions.CollectionExtensions;


namespace defaultdict
{
	public static class EnumerableExt
	{
		public static string GetString<T>( this IEnumerable<T> myEnum )
		{
			return string.Join(", ", myEnum );
		}
	}

    public static class RandomExt
    {
        //public const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string alphabet = "ABCDEFG";
        private static StringBuilder stringBuilder = new StringBuilder();

        public static char GetChar(this Random rng)
        {
            return alphabet[rng.Next(alphabet.Length)];
        }

        public static string GetString(this Random rng, int maxLen)
        {
            stringBuilder.Clear();

            int strLen = rng.Next(1,maxLen);
            for (int k = 0; k < strLen; k++)
            {
                stringBuilder.Append(rng.GetChar());
            }
            return stringBuilder.ToString();
        }

        public static IEnumerable<string> GetStrings(this Random rng, int numString, int maxLen)
        {
            for (int k = 0; k < numString; k++)
            {
                yield return rng.GetString(maxLen);
            }
        }
    }

	class MainClass
	{
		public static void Main (string[] args)
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

			//Console.WriteLine ("Hello World!");
            Console.ReadLine();
		}
	}
}
