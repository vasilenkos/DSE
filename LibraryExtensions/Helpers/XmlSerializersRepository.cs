using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace DSE.Extensions
{
    public class XmlSerializersRepository
    {
        protected static Dictionary<Type, XmlSerializer> _oRepository = new Dictionary<Type, XmlSerializer>();

        public static XmlSerializer Acquire(Type poType)
        {
            return _oRepository.ContainsKey(poType)
                ? _oRepository[poType]
                : new XmlSerializer(poType).Use(_ => _oRepository.Add(poType, _));
        }
    }
}