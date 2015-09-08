using System;

namespace RandomStringUtils
{
	/// <see cref="https://commons.apache.org/proper/commons-lang/javadocs/api-2.4/org/apache/commons/lang/RandomStringUtils.html"/>
	public static class RandomStringUtils
	{
		/// <summary>
		/// Represents a pseudo-random number generator, a device that produces a sequence
		/// of numbers that meet certain statistical requirements for randomness.
		/// </summary>
		/// <remarks>
		/// Use the RNGCryptoServiceProvider class if you need a strong random number generator.
		/// </remarks>
		private static readonly Random Rand = new Random();

		private const string Numeric = "0123456789";
		private const string Alphabetic = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
		private const string Symbols = " !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";

		/// <summary>
		/// Creates a random string whose length is the number of characters specified.
		///</summary>
		/// <param name="count">the length of random string to create</param>
		/// <returns>the random string</returns>
		public static string Random(int count)
		{
			var stringChars = new char[count];

			for (int i = 0; i < stringChars.Length; i++)
				stringChars[i] = Convert.ToChar(Rand.Next(Char.MaxValue));

			return new String(stringChars);
		}

		/// <summary>
		/// Creates a random string whose length is the number of characters specified.
		/// Characters will be chosen from the set of alpha-numeric characters as indicated by the arguments.
		/// </summary>
		/// <param name="count">the length of random string to create</param>
		/// <param name="letters">if true, generated string will include alphabetic characters</param>
		/// <param name="numbers">if true, generated string will include numeric characters </param>
		/// <returns>the random string</returns>
		public static string Random(int count, bool letters, bool numbers)
		{
			if (letters && numbers) return RandomAlphanumeric(count);
			if (letters) return RandomAlphabetic(count);
			if (numbers) return RandomNumeric(count);
			else
			{
				// the man does help us here.
				// I deal with it myself.
				return Random(count, Symbols);
			}
		}

		/// <summary>
		/// Creates a random string whose length is the number of characters specified.
		/// Characters will be chosen from the set of characters specified.
		/// </summary>
		/// <param name="count">the length of random string to create</param>
		/// <param name="chars">the String containing the set of characters to use, may be null</param>
		/// <returns>the random string</returns>
		/// <exception cref="ArgumentException">if count is lower than 0</exception>
		public static string Random(int count, char[] chars)
		{
			return Random(count, new string(chars));
		}

		/// <summary>
		/// Creates a random string whose length is the number of characters specified.
		/// Characters will be chosen from the set of alpha-numeric characters as indicated by the arguments.
		/// </summary>
		/// <param name="count">the length of random string to create</param>
		/// <param name="start">the position in set of chars to start at</param>
		/// <param name="end">the position in set of chars to end before</param>
		/// <param name="letters">if true, generated string will include alphabetic characters</param>
		/// <param name="numbers">if true, generated string will include numeric characters</param>
		/// <returns>the random string</returns>
		public static string Random(int count, int start, int end, bool letters, bool numbers)
		{
			var chars = (letters ? Alphabetic : String.Empty) + (numbers ? Numeric : String.Empty);
			return Random(count, chars.Substring(start, chars.Length - start - end));

		}

		/// <summary>
		/// Creates a random string based on a variety of options, using supplied source of randomness.
		/// This method has exactly the same semantics as Random(int,int,int,bool,bool,char[],Random),
		/// but instead of using an externally supplied source of randomness, it uses the internal static Random instance.
		/// </summary>
		/// <param name="count">the length of random string to create</param>
		/// <param name="start">the position in set of chars to start at</param>
		/// <param name="end">the position in set of chars to end before</param>
		/// <param name="letters">if true, generated string will include alphabetic characters</param>
		/// <param name="numbers">if true, generated string will include numeric characters</param>
		/// <param name="chars">the set of chars to choose randoms from. If null, then it will use the set of all chars.</param>
		/// <returns>the random string</returns>
		public static string Random(int count, int start, int end, bool letters, bool numbers, char[] chars)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Creates a random string based on a variety of options, using default source of randomness.
		/// </summary>
		/// <param name="count">the length of random string to create</param>
		/// <param name="start">the position in set of chars to start at</param>
		/// <param name="end">the position in set of chars to end before</param>
		/// <param name="letters">if true, generated string will include alphabetic characters</param>
		/// <param name="numbers">if true, generated string will include numeric characters</param>
		/// <param name="chars">the set of chars to choose randoms from. If null, then it will use the set of all chars.</param>
		/// <param name="random">a source of randomness.</param>
		/// <returns>the random string</returns>
		public static string Random(int count, int start, int end, bool letters, bool numbers, char[] chars, Random random)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Creates a random string whose length is the number of characters specified.
		/// </summary>
		/// <param name="count">the length of random string to create</param>
		/// <param name="chars"> the String containing the set of characters to use, may be null </param>
		/// <returns>the random string</returns>
		/// <exception cref="ArgumentException">if count is lower than 0</exception>
		public static string Random(int count, string chars)
		{
			if (count < 0) throw new ArgumentException();

			var stringChars = new char[count];

			for (var i = 0; i < stringChars.Length; i++)
				stringChars[i] = chars[Rand.Next(chars.Length)];

			return new String(stringChars);
		}

		/// <summary>
		/// Creates a random string whose length is the number of characters specified.
		/// </summary>
		/// <param name="count">the length of random string to create</param>
		/// <returns>the random string</returns>
		public static string RandomAlphabetic(int count)
		{
			return Random(count, Alphabetic);
		}

		/// <summary>
		/// Creates a random string whose length is the number of characters specified.
		/// Characters will be chosen from the set of alpha-numeric characters.
		/// </summary>
		/// <param name="count">the length of random string to create</param>
		/// <returns>the random string</returns>
		public static string RandomAlphanumeric(int count)
		{
			return Random(count, Alphabetic + Numeric);
		}

		/// <summary>
		/// Creates a random string whose length is the number of characters specified.
		/// Characters will be chosen from the set of characters whose ASCII value is between 32 and 126 (inclusive).
		/// </summary>
		/// <see cref="https://duckduckgo.com/?q=ascii+table&t=ffab"/>
		/// <param name="count">the length of random string to create</param>
		/// <returns>the random string</returns>
		public static string RandomAscii(int count)
		{
			return Random(count, Symbols + Alphabetic + Numeric);
		}

		/// <summary>
		/// Creates a random string whose length is the number of characters specified.
		/// Characters will be chosen from the set of numeric characters.
		/// </summary>
		/// <param name="count">the length of random string to create</param>
		/// <returns>the random string</returns>
		public static string RandomNumeric(int count)
		{
			return Random(count, Numeric);
		}
	}
}
