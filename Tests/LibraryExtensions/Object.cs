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

        [Test]
        public void DoTestCastAndUseIfNotNullAction()
        {
            var loFreelancer = Defaults.Class.DefaultFreelancer;

            loFreelancer.CastAndUseIfNotNull((Defaults.Class.Scientist _) => { Assert.Fail(); });
            loFreelancer.CastAndUseIfNotNull((Defaults.Class.Person _) => { Assert.IsNotNull(_); Assert.AreEqual(loFreelancer.FirstName, _.FirstName); });
            Assert.Catch<NullReferenceException>(() =>
            {
                loFreelancer.CastAndUseIfNotNull((Action<Defaults.Class.Person>)null);
            });
        }

        [Test]
        public void DoTestCastAndUseIfNotNullDefaultFunctor()
        {
            var loFreelancer = Defaults.Class.DefaultFreelancer;

            Assert.AreEqual(
                Defaults.Class.DefaultScientist,
                loFreelancer.CastAndUseIfNotNull(Defaults.Class.DefaultScientist, (Defaults.Class.Scientist _) => { Assert.Fail(); return _; })
            );

            Assert.AreEqual(
                loFreelancer,
                loFreelancer.CastAndUseIfNotNull(Defaults.Class.DefaultPerson1, (Defaults.Class.Person _) =>
                {
                    Assert.IsNotNull(_);
                    Assert.AreEqual(loFreelancer.FirstName, _.FirstName);

                    return _;
                })
            );

            Assert.Catch<NullReferenceException>(() =>
            {
                loFreelancer.CastAndUseIfNotNull(Defaults.Class.DefaultPerson1, (Func<Defaults.Class.Person, Defaults.Class.Person>)null);
            });
        }

        [Test]
        public void DoTestIfPredicateValue()
        {
            Assert.AreEqual(4, 3.If(_ => _ == 3, 4));
            Assert.AreEqual(3, 3.If(_ => _ != 3, 4));
            Assert.AreEqual(3, 3.If(null, 4));
        }

        [Test]
        public void DoTestIfPredicateValueElseValue()
        {
            Assert.AreEqual(4, 3.If(_ => _ == 3, 4, 5));
            Assert.AreEqual(5, 3.If(_ => _ != 3, 4, 5));
            Assert.AreEqual(3, 3.If(null, 4, 5));
        }

        [Test]
        public void DoTestSwitch()
        {
            var lnPassed = 0;

            // Fall-through mode without breaks
            // In the end we'll check if there are valid entries for asserts

            lnPassed = 0;

            3.Switch()
                // Fail section
                .Case(4, () => Assert.Fail())
                .Case(4, (_) => Assert.Fail())
                .Case(_ => _ == 4, () => Assert.Fail())
                .Case(_ => _ == 4, (_) => Assert.Fail())
                // Win section
                .Case(3, () => { lnPassed++; })
                .Case(3, (_) => { Assert.AreEqual(3, _); lnPassed++; })
                .Case(_ => _ == 3, () => { lnPassed++; })
                .Case(_ => _ == 3, (_) => { Assert.AreEqual(3, _); lnPassed++; })
                .Default(_ => { Assert.AreEqual(3, _); lnPassed++; });

            Assert.AreEqual(5, lnPassed);

            // Breaks mode

            lnPassed = 0;

            3.Switch()
                .BreakCase(3, () => { lnPassed++; })
                .BreakCase(3, (_) => { Assert.Fail(); })
                .BreakCase(_ => _ == 3, () => { Assert.Fail(); })
                .BreakCase(_ => _ == 3, (_) => { Assert.Fail(); })
                .Case(3, () => { Assert.Fail(); })
                .Default(_ => Assert.Fail());

            Assert.AreEqual(1, lnPassed);

            lnPassed = 0;

            3.Switch()
                .BreakCase(3, (_) => { lnPassed++; })
                .BreakCase(_ => _ == 3, () => { Assert.Fail(); })
                .BreakCase(_ => _ == 3, (_) => { Assert.Fail(); })
                .BreakCase(3, () => { Assert.Fail(); })
                .Case(3, () => { Assert.Fail(); })
                .Default(_ => Assert.Fail());

            Assert.AreEqual(1, lnPassed);

            lnPassed = 0;

            3.Switch()
                .BreakCase(_ => _ == 3, () => { lnPassed++; })
                .BreakCase(_ => _ == 3, (_) => { Assert.Fail(); })
                .BreakCase(3, () => { Assert.Fail(); })
                .BreakCase(3, (_) => { Assert.Fail(); })
                .Case(3, () => { Assert.Fail(); })
                .Default(_ => Assert.Fail());

            Assert.AreEqual(1, lnPassed);

            lnPassed = 0;

            3.Switch()
                .BreakCase(_ => _ == 3, (_) => { lnPassed++; })
                .BreakCase(3, () => { Assert.Fail(); })
                .BreakCase(3, (_) => { Assert.Fail(); })
                .BreakCase(_ => _ == 3, () => { Assert.Fail(); })
                .Case(3, () => { Assert.Fail(); })
                .Default(_ => Assert.Fail());

            Assert.AreEqual(1, lnPassed);
        }

        [Test]
        public void DoTestMatch()
        {
            var lsValue = String.Empty;

            lsValue =
                3.Match("3")
                    .Case(3, "C3")
                    .Case(_ => _ == 3, "C3P")
                    .Case(3, _ => "C3F")
                    .Case(_ => _ == 3, _ => "C3PF")
                .EndMatch();

            Assert.AreEqual("C3", lsValue);

            lsValue =
                3.Match("3")
                    .Case(_ => _ == 3, "C3P")
                    .Case(3, _ => "C3F")
                    .Case(_ => _ == 3, _ => "C3PF")
                    .Case(3, "C3P")
                .EndMatch();

            Assert.AreEqual("C3P", lsValue);

            lsValue =
                3.Match("3")
                    .Case(3, _ => "C3F")
                    .Case(_ => _ == 3, _ => "C3PF")
                    .Case(3, "C3")
                    .Case(_ => _ == 3, "C3P")
                .EndMatch();

            Assert.AreEqual("C3F", lsValue);

            lsValue =
                3.Match("3")
                    .Case(_ => _ == 3, _ => "C3PF")
                    .Case(3, "C3")
                    .Case(_ => _ == 3, "C3P")
                    .Case(3, _ => "C3F")
                .EndMatch();

            Assert.AreEqual("C3PF", lsValue);

            lsValue =
                3.Match("3")
                .EndMatch();

            Assert.AreEqual("3", lsValue);

            lsValue =
                3.Match("3")
                    .Case(4, "C4")
                .EndMatch();

            lsValue =
                3.Match("3")
                    .Case(4, "C4")
                .EndMatch((_,d) =>
                {
                    Assert.AreEqual(3, _);
                    Assert.AreEqual("3", d);

                    return "EM3";
                });

            Assert.AreEqual("EM3", lsValue);
        }
    }

    public class _Serialization : Symptomatic.SimpleTestCase
    {
        public override void BeginTestCase()
        {
            base.BeginTestCase();
            XmlSerializersRepository.Clear();
        }

        public override void EndTestCase()
        {
            XmlSerializersRepository.Clear();
            base.EndTestCase();
        }

        [Test]
        public void DoTestGetTypeAnatomy()
        {
            var loInt32TypeAnatomy = ((Int32)10).GetType().GetTypeAnatomy();
            var loGenericListTypeAnatomy = (new List<Int32>()).GetType().GetTypeAnatomy();

            Assert.IsNotNull(loInt32TypeAnatomy.TypeParameters);
            Assert.AreEqual(0, loInt32TypeAnatomy.TypeParameters.Length);
            Assert.AreEqual("System.Int32", loInt32TypeAnatomy.TypeName);
            Assert.IsNotNullOrEmpty(loInt32TypeAnatomy.TypeAssembly);

            Assert.IsNotNull(loGenericListTypeAnatomy.TypeParameters);
            Assert.AreEqual(1, loGenericListTypeAnatomy.TypeParameters.Length);
            Assert.AreEqual("System.Collections.Generic.List`1", loGenericListTypeAnatomy.TypeName);
            Assert.IsNotNullOrEmpty(loGenericListTypeAnatomy.TypeAssembly);
            Assert.IsNotNull(loGenericListTypeAnatomy.TypeParameters[0]);
            Assert.IsNotNull(loGenericListTypeAnatomy.TypeParameters[0]);
            Assert.AreEqual(0, loGenericListTypeAnatomy.TypeParameters[0].TypeParameters.Length);
            Assert.AreEqual("System.Int32", loGenericListTypeAnatomy.TypeParameters[0].TypeName);
            Assert.IsNotNullOrEmpty(loGenericListTypeAnatomy.TypeParameters[0].TypeAssembly);
        }

        [Serializable]
        public class Shmockup
        {
            public String Shmock;
            public String Shmick;

            public override bool Equals(object obj)
            {
                return obj
                    .CastAndUseIfNotNull(
                        false,
                        (Shmockup _) =>
                            true
                            && (_.Shmock == this.Shmock)
                            && (_.Shmick == this.Shmick)
                    );
            }
        }

        [Test]
        public void DoTestSerializeAndDeserialize()
        {
            var loObject = new Shmockup() { Shmock = "Shmock", Shmick = "Shmick" };
            var loSerializedByteStream = loObject.Serialize();
            var loDeserializedObject = loSerializedByteStream.Deserialize<Shmockup>();

            Assert.AreEqual(loObject, loDeserializedObject);
        }
    }
}