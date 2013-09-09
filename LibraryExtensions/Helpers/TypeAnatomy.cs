using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DSE.Extensions
{
    [Serializable]
    public class TypeAnatomy
    {
        public String TypeName;
        public String TypeAssembly;
        public TypeAnatomy[] TypeParameters;

        public TypeAnatomy()
            : this(null, null)
        {
        }

        public TypeAnatomy(String poTypeName, String poTypeAssembly, params TypeAnatomy[] paTypeParameters)
        {
            TypeName = poTypeName;
            TypeAssembly = poTypeAssembly;
            TypeParameters = paTypeParameters;
        }

        public Type CreateType()
        {
            var lsTypeName = this.GetTypeString();
            var loType = Type.GetType(lsTypeName, true, true);

            return loType;
        }

        public String GetTypeString()
        {
            return String.Format(
                "{0}{1}{2}",
                TypeName,
                TypeParameters
                    .Select(_ => String.IsNullOrEmpty(_.TypeAssembly) ? _.GetTypeString() : String.Format("[{0}]", _.GetTypeString()))
                    .ToArray()
                        .Join(",")
                        .Trim()
                        .UseIf(_ => !String.IsNullOrEmpty(_), String.Empty, _ => String.Format("[{0}]", _)),
                TypeAssembly.UseIf(_ => !String.IsNullOrEmpty(_), String.Empty, _ => String.Format(",{0}", _))
            );
        }

        public object CreateInstance(params object[] paParameters)
        {
            var loType = this.CreateType();

            return Activator.CreateInstance(loType, paParameters);
        }

        public TT CreateInstance<TT>(TT poDefaultValue, params object[] paParameters) where TT : class
        {
            return Activator.CreateInstance(this.CreateType(), paParameters).CastAndUseIfNotNull(poDefaultValue, (TT s) => s);
        }
    }
}