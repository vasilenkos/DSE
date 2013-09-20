using System;

using NUnit.Framework;
using DSE.Tests.Symptomatic;
using System.Collections.Generic;
using System.Linq;

using DSE.Extensions;

namespace DSE.Tests.LibraryExtensions
{
    /// <summary>
    /// Tests all IEnumerable extension methods
    /// </summary>
    public class _IEnumerable : Symptomatic.SimpleTestCase
    {
        [Test]
        public void DoTestApply()
        {
            var lnSum = (long)0;

            Defaults.IEnumerable.Filled.Int32.Apply(_ => lnSum += _);

            Assert.AreEqual(lnSum, Defaults.IEnumerable.Filled.Int32.Sum());

            var lnSumForParametrizedVersion = (long)0;
            var lnCount = (long)0;
            var lnFirstItemIndex = (long)-1;
            var lnLastItemIndex = (long)0;

            Defaults.IEnumerable.Filled.Int32.Apply((_, i) =>
            {
                lnSumForParametrizedVersion += _;
                lnCount++;
                lnFirstItemIndex = (lnFirstItemIndex == -1)
                    ? i
                    : lnFirstItemIndex;
                lnLastItemIndex = i;
            });

            Assert.AreEqual(lnSumForParametrizedVersion, Defaults.IEnumerable.Filled.Int32.Sum());
            Assert.AreEqual(lnCount, Defaults.IEnumerable.Filled.Int32.Count());
            Assert.AreEqual(lnFirstItemIndex, 0);
            Assert.AreEqual(lnLastItemIndex, Defaults.IEnumerable.Filled.Int32.Count() - 1);

            Assert.Catch<NullReferenceException>(() =>
            {
                Defaults.IEnumerable.Nulled.Int32.Apply(_ => { });
            });
        }

        [Test]
        public void DoTestMap()
        {
            var lnSum = 0;
            var loResult = Defaults.IEnumerable.Filled.Int32.Map(_ => { lnSum += _; return _; });

            // Sum must be a 0 before loResult traversing (because of lazyness)
            Assert.AreEqual(lnSum, 0);

            // Let's traverse like an imperative one...
            loResult.Apply(_ => { });

            // Now sum must to be a something instead of 0
            Assert.AreEqual(lnSum, Defaults.IEnumerable.Filled.Int32.Sum());

            Assert.DoesNotThrow(() =>
            {
                var loInvalidResult = Defaults.IEnumerable.Nulled.Int32.Map(_ => _);
            });

            Assert.Catch<NullReferenceException>(() =>
            {
                var loInvalidResult = Defaults.IEnumerable.Nulled.Int32.Map(_ => _);
                var lnCount = loInvalidResult.Count();
            });
        }

        [Test]
        public void DoTestAverageDefault()
        {
            Assert.AreEqual(Defaults.IEnumerable.Filled.Double.AverageDefault(10000d), -0.5d);
            Assert.AreEqual(Defaults.IEnumerable.Empty.Double.AverageDefault(10000d), 10000d);
            Assert.AreEqual(Defaults.IEnumerable.Nulled.Double.AverageDefault(10000d), 10000d);
        }

        [Test]
        public void DoTestMinDefault()
        {
            Assert.AreEqual(Defaults.IEnumerable.Filled.Double.MinDefault(10000d), -100d);
            Assert.AreEqual(Defaults.IEnumerable.Empty.Double.MinDefault(10000d), 10000d);
            Assert.AreEqual(Defaults.IEnumerable.Nulled.Double.MinDefault(10000d), 10000d);
        }

        [Test]
        public void DoTestMaxDefault()
        {
            Assert.AreEqual(Defaults.IEnumerable.Filled.Double.MaxDefault(10000d), 99d);
            Assert.AreEqual(Defaults.IEnumerable.Empty.Double.MaxDefault(10000d), 10000d);
            Assert.AreEqual(Defaults.IEnumerable.Nulled.Double.MaxDefault(10000d), 10000d);
        }

        [Test]
        public void DoTestMapToFirst()
        {
            var lnFetchedValue = 0;
            var loResult = Defaults.IEnumerable.Filled.Int32.MapToFirst(_ => { lnFetchedValue = _; return _; });

            // Sum must be a 0 before loResult traversing (because of lazyness)
            Assert.AreEqual(lnFetchedValue, 0);

            // Let's traverse like an imperative one...
            loResult.Apply(_ => { });

            // Now sum must to be a something instead of 0
            Assert.AreEqual(lnFetchedValue, Defaults.IEnumerable.Filled.Int32.First());
            Assert.AreEqual(loResult.Count(), 1);

            Assert.DoesNotThrow(() =>
            {
                var loInvalidResult = Defaults.IEnumerable.Nulled.Int32.MapToFirst(_ => _);
            });

            Assert.Catch<NullReferenceException>(() =>
            {
                var loInvalidResult = Defaults.IEnumerable.Nulled.Int32.MapToFirst(_ => _);
                var lnCount = loInvalidResult.Count();
            });
        }

        [Test]
        public void DoTestMapToLast()
        {
            var lnFetchedValue = 0;
            var loResult = Defaults.IEnumerable.Filled.Int32.MapToLast(_ => { lnFetchedValue = _; return _; });

            // Sum must be a 0 before loResult traversing (because of lazyness)
            Assert.AreEqual(lnFetchedValue, 0);

            // Let's traverse like an imperative one...
            loResult.Apply(_ => { });

            // Now sum must to be a something instead of 0
            Assert.AreEqual(lnFetchedValue, Defaults.IEnumerable.Filled.Int32.Last());

            Assert.Catch<NullReferenceException>(() =>
            {
                var loInvalidResult = Defaults.IEnumerable.Nulled.Int32.MapToLast(_ => _);
                var lnCount = loInvalidResult.Count();
            });
        }

        [Test]
        public void DoTestApplyToFirst()
        {
            var lnFetchedValue = 0;

            Defaults.IEnumerable.Filled.Int32.ApplyToFirst(_ => { lnFetchedValue = _; });

            Assert.AreEqual(lnFetchedValue, Defaults.IEnumerable.Filled.Int32.First());
            Assert.Catch<NullReferenceException>(() =>
            {
                Defaults.IEnumerable.Nulled.Int32.ApplyToFirst(_ => { });
            });
            Assert.Catch<NullReferenceException>(() =>
            {
                Defaults.IEnumerable.Filled.Int32.ApplyToFirst(null);
            });
            Assert.Catch<NullReferenceException>(() =>
            {
                Defaults.IEnumerable.Nulled.Int32.ApplyToFirst(null);
            });
        }

        [Test]
        public void DoTestApplyToRestAfterFirst()
        {
            var lnFetchedValue = 0;

            Defaults.IEnumerable.Filled.Int32.ApplyToRestAfterFirst(_ => { lnFetchedValue += _; });

            Assert.AreEqual(lnFetchedValue, 0);

            // Let's check lazy conversion
            lnFetchedValue = 0;
            Defaults.IEnumerable.Filled.Int32.ApplyToRestAfterFirst(_ => { lnFetchedValue += _; });

            Assert.AreEqual(lnFetchedValue, 0);

            Assert.Catch<NullReferenceException>(() =>
            {
                Defaults.IEnumerable.Nulled.Int32.ApplyToRestAfterFirst(_ => { });
            });
            Assert.Catch<NullReferenceException>(() =>
            {
                Defaults.IEnumerable.Filled.Int32.ApplyToRestAfterFirst(null);
            });
            Assert.Catch<NullReferenceException>(() =>
            {
                Defaults.IEnumerable.Nulled.Int32.ApplyToRestAfterFirst(null);
            });
        }

        [Test]
        public void DoTestApplyToLast()
        {
            var lnFetchedValue = 0;

            Defaults.IEnumerable.Filled.Int32.ApplyToLast(_ => { lnFetchedValue = _; });

            Assert.AreEqual(lnFetchedValue, Defaults.IEnumerable.Filled.Int32.Last());
            Assert.Catch<NullReferenceException>(() =>
            {
                Defaults.IEnumerable.Nulled.Int32.ApplyToLast(_ => { });
            });
            Assert.Catch<NullReferenceException>(() =>
            {
                Defaults.IEnumerable.Filled.Int32.ApplyToLast(null);
            });
            Assert.Catch<NullReferenceException>(() =>
            {
                Defaults.IEnumerable.Nulled.Int32.ApplyToLast(null);
            });
        }

        [Test]
        public void DoTestApplyToRestBeforeLast()
        {
            var lnFetchedValue = 0;

            Defaults.IEnumerable.Filled.Int32.ApplyToRestBeforeLast(_ => { lnFetchedValue += _; });

            Assert.AreEqual(lnFetchedValue, -199);

            // Let's check lazy conversion
            lnFetchedValue = 0;
            Defaults.IEnumerable.Filled.Int32.ApplyToRestBeforeLast(_ => { lnFetchedValue += _; });

            Assert.AreEqual(lnFetchedValue, -199);

            Assert.Catch<NullReferenceException>(() =>
            {
                Defaults.IEnumerable.Nulled.Int32.ApplyToRestBeforeLast(_ => { });
            });
            Assert.Catch<NullReferenceException>(() =>
            {
                Defaults.IEnumerable.Filled.Int32.ApplyToRestBeforeLast(null);
            });
            Assert.Catch<NullReferenceException>(() =>
            {
                Defaults.IEnumerable.Nulled.Int32.ApplyToRestBeforeLast(null);
            });
        }

    }
}