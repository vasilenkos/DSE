using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;
using DSE.Tests.Symptomatic;

using DSE.Extensions;

namespace DSE.Tests.LibraryExtensions
{
    public class _Sorter : Symptomatic.SimpleTestCase
    {
        [Test]
        public void DoTestSorter()
        {
            #if(!__MonoCS__)
            var loSequence = new List<Int32>() { 0, 477, 110, -36, 117, 252, -82, 2066 };

            loSequence.Sort(
                new Sorter<Int32>()
                    .ByExpression(_ => _ > 0, SorterDirection.Descending)
            );

            CollectionAssert.AreEqual(new List<Int32>() { 117, 252, 110, 2066, 477, 0, -82, -36 }, loSequence);

            loSequence.Sort(
                new Sorter<Int32>()
                    .ByExpression(_ => _ > 0, SorterDirection.Descending)
                    .ByComparison(_ => _)
            );

            CollectionAssert.AreEqual(new List<Int32>() { 110, 117, 252, 477, 2066, -82, -36, 0 }, loSequence);

            loSequence.Sort(
                new Sorter<Int32>()
                    .ByExpression(_ => _ > 0, SorterDirection.Descending)
                    .ByComparison((_1, _2) =>
                    {
                        if ((_1 > 0) && (_2 > 0))
                        {
                            if (_1 > _2) return 1;
                            if (_1 < _2) return -1;
                        }
                        else
                        {
                            if (_1 > _2) return -1;
                            if (_1 < _2) return 1;
                        }

                        return 0;
                    }, SorterDirection.Ascending)
            );

            CollectionAssert.AreEqual(new List<Int32>() { 110, 117, 252, 477, 2066, 0, -36, -82 }, loSequence);
            #endif
        }
    }
}