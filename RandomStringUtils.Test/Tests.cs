using System;
using System.Diagnostics;
using System.Linq;
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
