using System;
using System.Collections.Generic;
using System.IO;

using NUnit.Framework;
using DSE.Tests.Symptomatic;
using System.Xml.Serialization;
using System.Linq;

namespace DSE.Tests.LibraryExtensions.Helpers
{
    /// <summary>
    /// Just a simple test case for a SerializableDictionary.
    /// Checks if serialized and unserialized dictionaries are match;
    /// </summary>
    public class SerializableDictionary : SimpleTestCase
    {
        public class _TestClass
        {
            public String Id = String.Empty;
        }

        [Test]
        public void DoSerializeAndDeserializer()
        {
            var loSourceDictionary = new DSE.Extensions.SerializableDictionary<String, _TestClass>();

            foreach (var lsId in Enumerable.Range(0, 10).Select(_ => Guid.NewGuid().ToString()))
                loSourceDictionary.Add(lsId.ToUpper(), new _TestClass() { Id = lsId.ToLower() });

            var loSerializer = new XmlSerializer(loSourceDictionary.GetType());
            
            Assert.IsNotNull(loSerializer);

            var loWriterStream = new MemoryStream();
            var loWriter = new StreamWriter(loWriterStream);

            loSerializer.Serialize(loWriter, loSourceDictionary);
            
            var laContents = loWriterStream.ToArray();
            var loReaderStream = new MemoryStream(laContents);
            var loReader = new StreamReader(loReaderStream);
            
            Assert.IsNotEmpty(laContents);

            var loDeserializedObject = loSerializer.Deserialize(loReader) as DSE.Extensions.SerializableDictionary<String, _TestClass>;

            Assert.IsNotNull(loDeserializedObject);
            Assert.AreEqual(loSourceDictionary.Count, loDeserializedObject.Count);

            var laSourceArray = loSourceDictionary.ToArray();
            var laDestinationArray = loDeserializedObject.ToArray();

            for (var i=0; i<laSourceArray.Length; i++)
            {
                Assert.AreEqual(laSourceArray[i].Key, laDestinationArray[i].Key);
                Assert.AreEqual(laSourceArray[i].Value.Id, laDestinationArray[i].Value.Id);
            }
        }
    }
}