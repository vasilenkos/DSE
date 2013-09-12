using System;
using System.Collections.Generic;
using System.IO;

using NUnit.Framework;
using DSE.Tests.Symptomatic;
using System.Xml.Serialization;
using System.Linq;

using DSE.Extensions;

namespace DSE.Tests.LibraryExtensions.Helpers
{
    /// <summary>
    /// Test case for a type anatomy
    /// 
    /// 1. Let's create anatomy (and will ensure in it's not a null);
    /// 2. Let's check if anatomy-created object is not null.
    /// </summary>
    public class TypeAnatomy : Symptomatic.SimpleTestCase
    {
        [Test]
        public void DoCheck()
        {
            var loGenericItem = new List<List<String>>();
            var loTypeAnatomy = loGenericItem.GetType().GetTypeAnatomy();

            Assert.IsNotNull(loTypeAnatomy);

            var loGenericItemFromAnatomy = loTypeAnatomy.CreateInstance();

            Assert.IsTrue(loGenericItemFromAnatomy is List<List<String>>);
        }
    }
}