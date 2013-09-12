using System;

using NUnit.Framework;
using DSE.Tests.Symptomatic;
using System.Collections.Generic;

using DSE.Extensions;

namespace DSE.Tests.LibraryExtensions
{
    /// <summary>
    /// Tests all the kinds of a TryCall Func extension methods.
    /// </summary>
    public class _Func : Symptomatic.SimpleTestCase
    {
        [Test]
        public void DoTestTryCallOn()
        {
            var lfOkFunctor = new Func<String>(() => { return "Test"; });
            var lfFailFunctor = new Func<String>(() => { throw new NotImplementedException(); });

            Assert.AreEqual(lfOkFunctor.TryCall("failsafe"), "Test");
            Assert.AreEqual(lfFailFunctor.TryCall("failsafe"), "failsafe");
            Assert.Catch<NotImplementedException>(() => { 
                lfFailFunctor(); 
            });

            Assert.AreEqual(lfOkFunctor.TryCall("failsafe", (e, _) => { return "failsafe"; }), "Test");
            Assert.AreEqual(lfFailFunctor.TryCall("failsafe", (e, _) => { return "Test"; }), "Test");
            Assert.AreEqual(lfFailFunctor.TryCall("failsafe", (e, _) => { return e == null ? null : "Test"; }), "Test");
        }
    }
}