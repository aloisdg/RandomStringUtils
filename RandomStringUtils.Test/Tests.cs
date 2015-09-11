using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace RandomStringUtils.Test
{
	[TestFixture]
	public class Tests
	{
		private const int Count = 126;

		[Test]
		public void TestRandom()
		{
			Assert.AreEqual(Count, RandomStringUtils.Random(Count).Length);
		}

		[Test]
		public void TestRandomZero()
		{
			Assert.AreEqual(0, RandomStringUtils.Random(0).Length);
		}

		[Test]
		public void TestRandomError()
		{
			 Assert.Throws<ArgumentException>(() => RandomStringUtils.Random(-1));
		}

		[Test]
		public void TestRandomAlphabetic()
		{
			var result = RandomStringUtils.RandomAlphabetic(Count);
			Assert.IsTrue(Count == result.Length && result.All<Char>(Char.IsLetter));
		}

		[Test]
		public void TestRandomAlphanumeric()
		{
			var result = RandomStringUtils.RandomAlphanumeric(Count);
			Assert.IsTrue(Count == result.Length && result.All<Char>(Char.IsLetterOrDigit));
		}

		/// <summary>
		/// Check if RandomNumeric return a correct string made with digit.
		/// </summary>
		[Test]
		public void TestRandomNumeric()
		{
			var result = RandomStringUtils.RandomNumeric(Count);
			Assert.IsTrue(Count == result.Length && result.All<Char>(Char.IsDigit));
		}


		[Test]
		public void TestRandomAscii()
		{
			var result = RandomStringUtils.RandomAscii(Count);
			Assert.IsTrue(Count == result.Length && result.All<Char>(c => c >= 32 && c <= 126));
		}
	}
}
