using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace RandomStringUtils.Test
{
	[TestFixture]
	public class Tests
	{
		[Test]
		public void TestRandom()
		{
			Assert.AreEqual(42, RandomStringUtils.Random(42).Length);
		}
	}
}
