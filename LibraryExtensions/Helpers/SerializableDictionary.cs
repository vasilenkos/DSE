using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DSE.Extensions
{
    [XmlRoot("dictionary")]
    public class SerializableDictionary<K, V> : Dictionary<K, V>, IXmlSerializable
    {
        protected static XmlSerializer _oKeySerializer = new XmlSerializer(typeof(K));
        protected static XmlSerializer _oValueSerializer = new XmlSerializer(typeof(V));

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader poReader)
        {
            var lbWasEmpty = poReader.IsEmptyElement;

            poReader.Read();

            if (lbWasEmpty)
                return;

            while (poReader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                poReader.ReadStartElement("item");
                poReader.ReadStartElement("key");

                K loKey = (K)_oKeySerializer.Deserialize(poReader);

                poReader.ReadEndElement();
                poReader.ReadStartElement("value");

                V loValue = (V)_oValueSerializer.Deserialize(poReader);

                poReader.ReadEndElement();

                this.Add(loKey, loValue);

                poReader.ReadEndElement();
                poReader.MoveToContent();
            }

            poReader.ReadEndElement();
        }

        public void WriteXml(System.Xml.XmlWriter poWriter)
        {
            foreach (K loKey in this.Keys)
            {
                poWriter.WriteStartElement("item");
                poWriter.WriteStartElement("key");

                _oKeySerializer.Serialize(poWriter, loKey);

                poWriter.WriteEndElement();
                poWriter.WriteStartElement("value");

                V loValue = this[loKey];

                _oValueSerializer.Serialize(poWriter, loValue);

                poWriter.WriteEndElement();
                poWriter.WriteEndElement();
            }
        }
    }
}