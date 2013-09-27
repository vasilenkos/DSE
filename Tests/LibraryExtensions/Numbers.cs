using System;

using NUnit.Framework;
using DSE.Tests.Symptomatic;

using DSE.Extensions;

namespace DSE.Tests.LibraryExtensions
{
    public class _Numbers : Symptomatic.SimpleTestCase
    {
        [Test]
        public void DoTestBetween_Decimal()
        {
            var lnValue = (decimal)250;

            Assert.IsTrue(lnValue.Between(250, 250));
            Assert.IsTrue(lnValue.Between(-100, 250));
            Assert.IsTrue(lnValue.Between(250, 350));
            Assert.IsTrue(lnValue.Between(-100, 350));
            Assert.IsFalse(lnValue.Between(350, -100));
            Assert.IsFalse(lnValue.Between(-10, 10));
            Assert.IsFalse(lnValue.Between(350, 360));
        }

        [Test]
        public void DoTestBetween_Int()
        {
            var lnValue = (int)250;

            Assert.IsTrue(lnValue.Between(250, 250));
            Assert.IsTrue(lnValue.Between(-100, 250));
            Assert.IsTrue(lnValue.Between(250, 350));
            Assert.IsTrue(lnValue.Between(-100, 350));
            Assert.IsFalse(lnValue.Between(350, -100));
            Assert.IsFalse(lnValue.Between(-10, 10));
            Assert.IsFalse(lnValue.Between(350, 360));
        }

        [Test]
        public void DoTestBetween_Float()
        {
            var lnValue = (float)250;

            Assert.IsTrue(lnValue.Between(250, 250));
            Assert.IsTrue(lnValue.Between(-100, 250));
            Assert.IsTrue(lnValue.Between(250, 350));
            Assert.IsTrue(lnValue.Between(-100, 350));
            Assert.IsFalse(lnValue.Between(350, -100));
            Assert.IsFalse(lnValue.Between(-10, 10));
            Assert.IsFalse(lnValue.Between(350, 360));
        }

        [Test]
        public void DoTestBetween_Double()
        {
            var lnValue = (double)250;

            Assert.IsTrue(lnValue.Between(250, 250));
            Assert.IsTrue(lnValue.Between(-100, 250));
            Assert.IsTrue(lnValue.Between(250, 350));
            Assert.IsTrue(lnValue.Between(-100, 350));
            Assert.IsFalse(lnValue.Between(350, -100));
            Assert.IsFalse(lnValue.Between(-10, 10));
            Assert.IsFalse(lnValue.Between(350, 360));
        }

        [Test]
        public void DoTestClampWithRange_Decimal()
        {
            var lnValue = (decimal)250;

            Assert.AreEqual(lnValue.ClampWithRange(220,280), 250);
            Assert.AreEqual(lnValue.ClampWithRange(150, 200), 200);
            Assert.AreEqual(lnValue.ClampWithRange(350, 400), 350);
        }

        [Test]
        public void DoTestClampWithRange_Int()
        {
            var lnValue = (int)250;

            Assert.AreEqual(lnValue.ClampWithRange(220, 280), 250);
            Assert.AreEqual(lnValue.ClampWithRange(150, 200), 200);
            Assert.AreEqual(lnValue.ClampWithRange(350, 400), 350);
        }

        [Test]
        public void DoTestClampWithRange_Float()
        {
            var lnValue = (float)250;

            Assert.AreEqual(lnValue.ClampWithRange(220, 280), 250);
            Assert.AreEqual(lnValue.ClampWithRange(150, 200), 200);
            Assert.AreEqual(lnValue.ClampWithRange(350, 400), 350);
        }

        [Test]
        public void DoTestClampWithRange_Double()
        {
            var lnValue = (double)250;

            Assert.AreEqual(lnValue.ClampWithRange(220, 280), 250);
            Assert.AreEqual(lnValue.ClampWithRange(150, 200), 200);
            Assert.AreEqual(lnValue.ClampWithRange(350, 400), 350);
        }
    }
}