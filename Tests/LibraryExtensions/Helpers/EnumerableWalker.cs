using System;

using NUnit.Framework;
using DSE.Tests.Symptomatic;

namespace DSE.Tests.LibraryExtensions.Helpers
{
    public class EnumerableWalker : SimpleTestCase
    {
        public class _Context
        {
        }

        [Test]
        public void StringCreationTest()
        {
            var loData = Defaults.IEnumerable.String;
            var loContext = new _Context();
            var loWalker = new DSE.Extensions.EnumerableWalker<String, _Context>(loData, loContext);
        }

        [Test]
        public void Int32CreationTest()
        {
            var loData = Defaults.IEnumerable.Int32;
            var loContext = new _Context();
            var loWalker = new DSE.Extensions.EnumerableWalker<Int32, _Context>(loData, loContext);
        }
    }
}