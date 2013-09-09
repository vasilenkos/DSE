using System;

using NUnit.Framework;
using DSE.Tests.Symptomatic;
using System.Collections.Generic;

using DSE.Extensions;

namespace DSE.Tests.LibraryExtensions
{
    public class _DateTime : Symptomatic.SimpleTestCase
    {
        [Test]
        public void DoTestBetween()
        {
            var loNow = DateTime.Now;
            var loLesserDate = loNow.AddDays(-1);
            var loGreaterDate = loNow.AddDays(1);

            Assert.IsTrue(loNow.Between(null, null));
            Assert.IsTrue(loNow.Between(null, loGreaterDate));
            Assert.IsFalse(loNow.Between(null, loLesserDate));
            Assert.IsTrue(loNow.Between(loLesserDate, null));
            Assert.IsFalse(loNow.Between(loGreaterDate, null));
            Assert.IsTrue(loNow.Between(loLesserDate, loGreaterDate));
            Assert.IsFalse(loNow.Between(loGreaterDate, loLesserDate));
            Assert.IsTrue(loNow.Between(loNow, loGreaterDate));
            Assert.IsTrue(loNow.Between(loLesserDate, loNow));
            Assert.IsTrue(loNow.Between(loNow, loNow));
        }
    }
}