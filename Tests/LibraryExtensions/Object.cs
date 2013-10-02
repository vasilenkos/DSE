using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;
using DSE.Tests.Symptomatic;

using DSE.Extensions;

namespace DSE.Tests.LibraryExtensions
{
    public class _Object : Symptomatic.SimpleTestCase
    {
        [Test]
        public void DoTestChain()
        {
            var loCapturedObject = (Defaults.Class.Person)null;
            var loReturnedObject = Defaults.Class.DefaultPerson1.Chain(_ => loCapturedObject = _);

            Assert.AreEqual(loReturnedObject, Defaults.Class.DefaultPerson1);
            Assert.AreEqual(loCapturedObject, Defaults.Class.DefaultPerson1);

            Assert.IsNull(((object)null).Chain(_ => { }));
            Assert.Catch<NullReferenceException>(() =>
            {
                Defaults.Class.DefaultPerson1.Chain(null);
            });
        }

        [Test]
        public void DoTestUseAction()
        {
            var loCapturedObject = (Defaults.Class.Person)null;
            var loReturnedObject = Defaults.Class.DefaultPerson1.Use(_ => { loCapturedObject = _; });

            Assert.AreEqual(loReturnedObject, Defaults.Class.DefaultPerson1);
            Assert.AreEqual(loCapturedObject, Defaults.Class.DefaultPerson1);

            Assert.IsNull(((object)null).Use(_ => { }));
            Assert.Catch<NullReferenceException>(() =>
            {
                Defaults.Class.DefaultPerson1.Use((Action<Defaults.Class.Person>)null);
            });
        }

        [Test]
        public void DoTestUseFunc()
        {
            var loCapturedObject = (Defaults.Class.Person)null;
            var loReturnedObject = Defaults.Class.DefaultPerson1.Use(_ => { return loCapturedObject = _; });

            Assert.AreEqual(loReturnedObject, Defaults.Class.DefaultPerson1);
            Assert.AreEqual(loCapturedObject, Defaults.Class.DefaultPerson1);

            Assert.IsNull(Defaults.Class.DefaultPerson1.Use(_ => null));
            Assert.IsNull(((object)null).Use(_ => { }));
            Assert.Catch<NullReferenceException>(() =>
            {
                Defaults.Class.DefaultPerson1.Use((Action<Defaults.Class.Person>)null);
            });
        }

        [Test]
        public void DoTestUseIfNotNullFunc()
        {
            var loNullObject = (Defaults.Class.Person)null;
            var loNotNullObject = Defaults.Class.DefaultPerson1;

            Assert.IsNull(loNullObject.UseIfNotNull(_ => loNotNullObject));
            Assert.IsNull(loNotNullObject.UseIfNotNull(_ => loNullObject));
            Assert.IsNotNull(loNotNullObject.UseIfNotNull(_ => _));
            Assert.IsNull(loNotNullObject.UseIfNotNull((Func<Defaults.Class.Person, Defaults.Class.Person>)null));
        }

        [Test]
        public void DoTestUseIfNotNullDefaultFunc()
        {
            var loNullObject = (Defaults.Class.Person)null;
            var loNotNullObject1 = Defaults.Class.DefaultPerson1;
            var loNotNullObject2 = Defaults.Class.DefaultPerson2;
            var loNotNullObject3 = Defaults.Class.DefaultPerson3;

            Assert.AreEqual(loNotNullObject1, loNullObject.UseIfNotNull(loNotNullObject1, _ => loNotNullObject2));
            Assert.AreEqual(loNotNullObject2, loNotNullObject2.UseIfNotNull(loNotNullObject1, _ => _));
            Assert.AreEqual(loNotNullObject3, loNotNullObject2.UseIfNotNull(loNotNullObject1, _ => loNotNullObject3));
            Assert.AreEqual(loNotNullObject2, loNotNullObject1.UseIfNotNull(loNotNullObject2, (Func<Defaults.Class.Person, Defaults.Class.Person>)null));
        }

        [Test]
        public void DoTestUseIfNotNullAction()
        {
            var loNullObject = (Defaults.Class.Person)null;
            var loNotNullObject1 = Defaults.Class.DefaultPerson1;

            loNullObject.UseIfNotNull(_ =>
            {
                Assert.Fail();
            });

            loNotNullObject1.UseIfNotNull(_ =>
            {
                Assert.AreEqual(loNotNullObject1, _);
            });
        }

        [Test]
        public void DoTestUseIfPredicateFunctor()
        {
            var loNullObject = (Defaults.Class.Person)null;
            var loNotNullObject1 = Defaults.Class.DefaultPerson1;

            Assert.IsNull(loNullObject.UseIf(_ => _ != null, _ => _));
            Assert.IsNotNull(loNullObject.UseIf(_ => _ == null, _ => loNotNullObject1));
            Assert.IsNull(loNullObject.UseIf(_ => _ != null, _ => loNotNullObject1));
            Assert.IsNull(loNullObject.UseIf(null, (Func<Defaults.Class.Person, Defaults.Class.Person>)null));
        }

        [Test]
        public void DoTestUseIfPredicateAction()
        {
            var loNullObject = (Defaults.Class.Person)null;
            var loNotNullObject1 = Defaults.Class.DefaultPerson1;

            loNullObject.UseIf(_ => _ != null, _ => Assert.Fail());
            loNullObject.UseIf(_ => _ == null, _ => Assert.IsNull(_));
            loNotNullObject1.UseIf(_ => _ != null, _ => Assert.IsNotNull(_));
            loNotNullObject1.UseIf(_ => _ == null, _ => Assert.Fail());
        }

        [Test]
        public void DoTestUseIfPredicateDefaultFunctor()
        {
            var loNullObject = (Defaults.Class.Person)null;
            var loNotNullObject1 = Defaults.Class.DefaultPerson1;
            var loNotNullObject2 = Defaults.Class.DefaultPerson2;
            var loNotNullObject3 = Defaults.Class.DefaultPerson3;

            Assert.AreEqual(loNotNullObject2, loNullObject.UseIf(_ => _ != null, loNotNullObject2, _ => _));
            Assert.AreEqual(loNotNullObject1, loNullObject.UseIf(_ => _ == null, loNotNullObject2, _ => loNotNullObject1));
            Assert.AreEqual(loNotNullObject2, loNullObject.UseIf(_ => _ != null, loNotNullObject2, _ => loNotNullObject1));
            Assert.IsNull(loNullObject.UseIf(null, null, (Func<Defaults.Class.Person, Defaults.Class.Person>)null));
            Assert.AreEqual(loNotNullObject1, loNullObject.UseIf(null, loNotNullObject1, (Func<Defaults.Class.Person, Defaults.Class.Person>)null));
        }
    }
}