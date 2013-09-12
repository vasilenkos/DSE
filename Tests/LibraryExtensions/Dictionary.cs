using System;

using NUnit.Framework;
using DSE.Tests.Symptomatic;
using System.Collections.Generic;

using DSE.Extensions;

namespace DSE.Tests.LibraryExtensions
{
    /// <summary>
    /// Tests all the kinds of a TryGetValue Dictionary&lt;&gt; extension methods.
    /// </summary>
    public class _Dictionary : Symptomatic.SimpleTestCase
    {
        [Test]
        public void DoTestTryGetValue()
        {
            var loDictionary = new Dictionary<String, String>()
            {
                { "Element1", "Value1"},
                { "Element2", "Value2"},
                { "Element3", "Value3"},
            };

            Assert.AreEqual(loDictionary.TryGetValue("Non-existent", null), null);
            Assert.AreEqual(loDictionary.TryGetValue("Non-existent", "1"), "1");
            Assert.AreEqual(loDictionary.TryGetValue("Non-existent", null, _ => _), null);
            Assert.AreEqual(loDictionary.TryGetValue("Non-existent", null, _ => _), null);
            Assert.AreEqual(loDictionary.TryGetValue("Element1", null, _ => _.ToLower()), "value1");
            Assert.Catch<System.NullReferenceException>(() =>
            {
                loDictionary.TryGetValue("Element1", null, null);
            });
        }
    }
}