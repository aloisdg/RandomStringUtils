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

		/// <summary>
		/// Creates a random string whose length is the number of characters specified.
		///</summary>
		/// <param name="count">the length of random string to create</param>
		/// <returns>the random string</returns>
		public static string Random(int count)
		{
			return Random(count, false, false);
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
			return Random(count, 0, 0, letters, numbers);
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
			return Random(count, start, end, letters, numbers, null, Rand);
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
			return Random(count, start, end, letters, numbers, chars, Rand);
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
			if (count == 0)
				return String.Empty;
			if (count < 0)
				throw new ArgumentException("Requested random string length " + count + " is less than 0.");
			if (start == 0 && end == 0)
			{
				if (!letters && !numbers)
				{
					end = Int32.MaxValue;
					start = 0;
				}
				else
				{
					end = 'z' + 1;
					start = ' ';
				}
			}

			var buffer = new char[count];
			var gap = end - start;

			while (count-- != 0)
			{
				var ch = (chars == null) ? (char)(random.Next(gap) + start) : chars[random.Next(gap) + start];
				if ((letters && Char.IsLetter(ch)) || (numbers && Char.IsDigit(ch)) || (!letters && !numbers))
				{
					if (ch >= 56320 && ch <= 57343)
					{
						if (count == 0) count++;
						else
						{
							// low surrogate, insert high surrogate after putting it in
							buffer[count] = ch;
							count--;
							buffer[count] = (char)(55296 + random.Next(128));
						}
					}
					else if (ch >= 55296 && ch <= 56191)
					{
						if (count == 0) count++;
						else
						{
							// high surrogate, insert low surrogate before putting it in
							buffer[count] = (char)(56320 + random.Next(128));
							count--;
							buffer[count] = ch;
						}
					}
					else if (ch >= 56192 && ch <= 56319)
						// private high surrogate, no effing clue, so skip it
						count++;
					else
						buffer[count] = ch;
				}
				else
					count++;
			}
			return new String(buffer);
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
			return chars == null
				? Random(count, 0, 0, false, false, null, Rand)
				: Random(count, chars.ToCharArray());
		}

		/// <summary>
		/// Creates a random string whose length is the number of characters specified.
		/// </summary>
		/// <param name="count">the length of random string to create</param>
		/// <returns>the random string</returns>
		public static string RandomAlphabetic(int count)
		{
			return Random(count, true, false);
		}

		/// <summary>
		/// Creates a random string whose length is the number of characters specified.
		/// Characters will be chosen from the set of alpha-numeric characters.
		/// </summary>
		/// <param name="count">the length of random string to create</param>
		/// <returns>the random string</returns>
		public static string RandomAlphanumeric(int count)
		{
			return Random(count, true, true);
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
			return Random(count, 32, 127, false, false);
		}

		/// <summary>
		/// Creates a random string whose length is the number of characters specified.
		/// Characters will be chosen from the set of numeric characters.
		/// </summary>
		/// <param name="count">the length of random string to create</param>
		/// <returns>the random string</returns>
		public static string RandomNumeric(int count)
		{
			return Random(count, false, true);
		}
	}
}
