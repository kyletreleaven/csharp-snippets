using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpSnippets.Extensions.RandomExt
{
	public static class RandomExt
	{
		public const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		//public const string alphabet = "ABCDEFG";

		public static char GetChar(this System.Random rng, string alphabet=ALPHABET )
		{
			return alphabet[rng.Next(alphabet.Length)];
		}

		public static string GetkString(this Random rng, int strLen, string alphabet=ALPHABET)
		{
			var sb = new StringBuilder ();

			for (int k = 0; k < strLen; k++)
			{
				sb.Append( rng.GetChar(alphabet) );
			}
			return sb.ToString();
		}

		public static string GetString(this Random rng, int maxLen, string alphabet=ALPHABET)
		{
			return rng.GetkString (rng.Next (1, maxLen), alphabet);
		}

		public static IEnumerable<string> GetStrings(this Random rng, int numString, int maxLen, string alphabet=ALPHABET)
		{
			for (int k = 0; k < numString; k++)
			{
				yield return rng.GetString(maxLen, alphabet);
			}
		}
	}
}

