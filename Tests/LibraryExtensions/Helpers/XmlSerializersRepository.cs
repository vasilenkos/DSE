using System;
using System.Collections.Generic;
using System.IO;

using NUnit.Framework;
using DSE.Tests.Symptomatic;
using System.Xml.Serialization;
using System.Linq;

namespace DSE.Tests.LibraryExtensions.Helpers
{
    public class XmlSerializersRepository : Symptomatic.SimpleTestCase
    {
        public class _TestClass
        {
        }

        [Test]
        public void DoCheck()
        {
            Assert.AreSame(
                DSE.Extensions.XmlSerializersRepository.Acquire(typeof(_TestClass)),
                DSE.Extensions.XmlSerializersRepository.Acquire(typeof(_TestClass))
            );
        }
    }
}