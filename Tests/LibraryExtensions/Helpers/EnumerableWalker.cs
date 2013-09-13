using System;

using NUnit.Framework;
using DSE.Tests.Symptomatic;
using System.Collections.Generic;

namespace DSE.Tests.LibraryExtensions.Helpers
{
    /// <summary>
    /// Just a simple test case for an EnumerableWalker.
    /// 1. Checks OnBegin/OnEnd logic (cont of their calls must to be equal);
    /// 2. Checks if all items are walked;
    /// 3. Checks if group walk is performed well (e.g. measures count of OnGroupBegin/OnGroupEnd calls);
    /// </summary>
    public class EnumerableWalker : SimpleTestCase
    {
        // Test context
        public class _Context
        {
            public String SaltBefore = String.Empty;
            public String SaltAfter = String.Empty;
            public Int32 CountBefore = 0;
            public Int32 CountAfter = 0;
            public Int32 Count = 0;
            public List<String> Items = new List<String>();
        }

        // Test group
        public class _Group
        {
            public Int32 CountBefore = 0;
            public Int32 CountAfter = 0;
        }

        [Test]
        public void StringsDemonstration()
        {
            var loData = Defaults.IEnumerable.Filled.String;
            var loContext = new _Context();
            var loWalker = new DSE.Extensions.EnumerableWalker<String, _Context>(loData, loContext);
            var lsTestValueOnBegin = "Begin";
            var lsTestValueOnEnd = "End";
            var loTestGroup = new _Group();

            loWalker
                .OnBegin(_ => { _.SaltBefore = lsTestValueOnBegin; _.CountBefore++; })
                .OnItem((_, s) => { _.Count++; _.Items.Add(s); })
                .OnGroup(loTestGroup, (c, g, s) => !String.IsNullOrEmpty(s) && (s[0] == '2'), (c, g, s) => g.CountBefore++, (c, g) => g.CountAfter++)
                .OnEnd(_ => { _.SaltAfter = lsTestValueOnEnd; _.CountAfter++; })
                    .Walk();

            Assert.AreEqual(loContext.CountBefore, 1);
            Assert.AreEqual(loContext.SaltBefore, lsTestValueOnBegin);
            Assert.AreEqual(loContext.CountAfter, 1);
            Assert.AreEqual(loContext.SaltAfter, lsTestValueOnEnd);

            var loTestList = new List<String>();
            var lnGroupHit = 0;

            foreach (var loItem in loData)
                loTestList.Add(loItem);

            Assert.AreEqual(loContext.Count, loTestList.Count);

            for (var i = 0; i < loTestList.Count; i++)
            {
                var lsValue = loTestList[i];

                Assert.AreEqual(lsValue, loContext.Items[i]);

                if (!String.IsNullOrEmpty(lsValue) && lsValue[0] == '2')
                {
                    lnGroupHit++;
                }
            }

            Assert.AreEqual(loTestGroup.CountBefore, lnGroupHit);
            Assert.AreEqual(loTestGroup.CountAfter, lnGroupHit);
        }
    }
}
