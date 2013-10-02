using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.IO;

namespace DSE.Extensions
{
    public static class _Object
    {
        #region Control-flow

        public static T Chain<T>(this T poObject, Action<T> poFunctor)
        {
            poFunctor(poObject);

            return poObject;
        }

        public static T Use<T>(this T poObject, Func<T, T> poFunctor) where T : class
        {
            return poFunctor(poObject);
        }

        public static T Use<T>(this T poObject, Action<T> poFunctor) where T : class
        {
            poFunctor(poObject);

            return poObject;
        }

        public static T UseIfNotNull<T>(this T poObject, Func<T, T> poFunctor) where T : class
        {
            return (false
                || (poFunctor == null)
                || (poObject == null)
            )
                ? null
                : poFunctor(poObject);
        }

        public static P UseIfNotNull<T, P>(this T poObject, P poDefault, Func<T, P> poFunctor) where T : class
        {
            return (false
                || (poFunctor == null)
                || (poObject == null)
            )
                ? poDefault
                : poFunctor(poObject);
        }

        public static void UseIfNotNull<T>(this T poObject, Action<T> poFunctor) where T : class
        {
            if (false
                || (poFunctor == null)
                || (poObject == null)
            ) return;

            poFunctor(poObject);
        }

        public static T UseIf<T>(this T poObject, Func<T, bool> poPredicate, Func<T, T> poFunctor) where T : class
        {
            return (false
                || (poFunctor == null)
                || (poPredicate == null)
            )
                ? null
                : poPredicate(poObject)
                    ? poFunctor(poObject)
                    : poObject;
        }

        public static void UseIf<T>(this T poObject, Func<T, bool> poPredicate, Action<T> poFunctor)
        {
            if (false
                || (poFunctor == null)
                || (poPredicate == null)
            ) return;

            if (poPredicate(poObject))
            {
                poFunctor(poObject);
            }
        }

        public static P UseIf<T, P>(this T poObject, Func<T, bool> poPredicate, P poDefault, Func<T, P> poFunctor)
        {
            if (false
                || (poFunctor == null)
                || (poPredicate == null)
            ) return poDefault;

            return poPredicate(poObject)
                ? poFunctor(poObject)
                : poDefault;
        }

        public static void CastAndUseIfNotNull<T, P>(this T poObject, Action<P> poFunctor)
            where T : class
            where P : class
        {
            var loCastResult = (poObject as P);

            if (loCastResult != null)
            {
                poFunctor(loCastResult);
            }
        }

        public static S CastAndUseIfNotNull<T, P, S>(this T poObject, S poDefault, Func<P, S> poFunctor)
            where T : class
            where P : class
        {
            var loCastResult = (poObject as P);

            if (loCastResult != null)
            {
                return poFunctor(loCastResult);
            }

            return poDefault;
        }

        public static T If<T>(this T poObject, Func<T, bool> poPredicate, T poValue)
        {
            if (false
                || (poPredicate == null)
            ) return poObject;

            return poPredicate(poObject)
                ? poValue
                : poObject;
        }

        public static T If<T>(this T poObject, Func<T, bool> poPredicate, T poValue, T poElseValue)
        {
            if (false
                || (poPredicate == null)
            ) return poObject;

            return poPredicate(poObject)
                ? poValue
                : poElseValue;
        }

        #endregion

        #region Switch/case/default

        #region Void switch

        public class _Switch<T>
        {
            public T Contents { get; private set; }

            public _Switch(T poObject)
            {
                Contents = poObject;
            }
        }

        public static _Switch<T> Switch<T>(this T poObject)
        {
            return new _Switch<T>(poObject);
        }

        public static _Switch<T> Case<T>(this _Switch<T> poSwitch, T poValue, Action<T> poAction)
        {
            return poSwitch.Case(o => o.Equals(poValue), poAction);
        }

        public static _Switch<T> Case<T>(this _Switch<T> poSwitch, T poValue, Action poAction)
        {
            return poSwitch.Case(o => o.Equals(poValue), poAction);
        }

        public static _Switch<T> Case<T>(this _Switch<T> poSwitch, Func<T, Boolean> poPredicate, Action poAction)
        {
            return Case(poSwitch, poPredicate, o => poAction());
        }

        public static _Switch<T> Case<T>(this _Switch<T> poSwitch, Func<T, Boolean> poPredicate, Action<T> poAction)
        {
            if (poSwitch == null)
            {
                return null;
            }

            if (poPredicate(poSwitch.Contents))
            {
                poAction(poSwitch.Contents);
            }

            return poSwitch;
        }

        public static _Switch<T> BreakCase<T>(this _Switch<T> poSwitch, T poValue, Action<T> poAction)
        {
            return poSwitch.BreakCase(o => o.Equals(poValue), poAction);
        }

        public static _Switch<T> BreakCase<T>(this _Switch<T> poSwitch, T poValue, Action poAction)
        {
            return poSwitch.BreakCase(o => o.Equals(poValue), poAction);
        }

        public static _Switch<T> BreakCase<T>(this _Switch<T> poSwitch, Func<T, Boolean> poPredicate, Action poAction)
        {
            return Case(poSwitch, poPredicate, o => poAction());
        }

        public static _Switch<T> BreakCase<T>(this _Switch<T> poSwitch, Func<T, Boolean> poPredicate, Action<T> poAction)
        {
            if (poSwitch == null)
            {
                return null;
            }

            if (poPredicate(poSwitch.Contents))
            {
                poAction(poSwitch.Contents);
                return null;
            }

            return poSwitch;
        }

        public static void Default<T>(this _Switch<T> poSwitch, Action<T> poAction)
        {
            if (poSwitch == null)
            {
                return;
            }

            poAction(poSwitch.Contents);

            return;
        }

        #endregion

        #region Data switch

        public class _Match<T, P>
        {
            public T Contents { get; private set; }
            public P Value { get { return _oValue; } set { if (!HasValue) { _oValue = value; HasValue = true; } } }
            public Boolean HasValue { get; private set; }
            public P DefaultValue { get; private set; }

            protected P _oValue;

            public _Match(T poObject, P poDefault)
            {
                HasValue = false;
                Contents = poObject;
                DefaultValue = poDefault;
            }
        }

        public static _Match<T, P> Match<T, P>(this T poObject, P poDefault)
        {
            return new _Match<T, P>(poObject, poDefault);
        }

        public static _Match<T, P> Case<T, P>(this _Match<T, P> poSwitch, T poValue, Func<T, P> poAction)
        {
            return poSwitch.Case(o => o == null ? poValue == null : o.Equals(poValue), poAction);
        }

        public static _Match<T, P> Case<T, P>(this _Match<T, P> poSwitch, T poValue, P poReturnValue)
        {
            return poSwitch.Case(o => o == null ? poValue == null : o.Equals(poValue), poReturnValue);
        }

        public static _Match<T, P> Case<T, P>(this _Match<T, P> poSwitch, Func<T, Boolean> poPredicate, P poReturnValue)
        {
            return poSwitch.Case(poPredicate, o => poReturnValue);
        }

        public static _Match<T, P> Case<T, P>(this _Match<T, P> poSwitch, Func<T, Boolean> poPredicate, Func<T, P> poAction)
        {
            if (poSwitch == null)
            {
                return null;
            }

            if (!poSwitch.HasValue)
            {
                if (poPredicate(poSwitch.Contents))
                {
                    poSwitch.Value = poAction(poSwitch.Contents);
                }
            }

            return poSwitch;
        }

        public static P EndMatch<T, P>(this _Match<T, P> poSwitch, Func<T, P, P> poAction)
        {
            if (poSwitch == null)
            {
                return default(P);
            }

            if (poSwitch.HasValue)
            {
                return poSwitch.Value;
            }

            return poAction(poSwitch.Contents, poSwitch.DefaultValue);
        }

        public static P EndMatch<T, P>(this _Match<T, P> poSwitch)
        {
            if (poSwitch == null)
            {
                return default(P);
            }

            if (poSwitch.HasValue)
            {
                return poSwitch.Value;
            }

            return poSwitch.DefaultValue;
        }
        #endregion

        #endregion

        public static TypeAnatomy GetTypeAnatomy<T>(this T poType) where T : Type
        {
            return
                new TypeAnatomy(
                    (poType.IsGenericType ? poType.GetGenericTypeDefinition() : poType).FullName,
                    poType.Assembly.FullName,
                    poType.IsGenericType
                        ? poType.GetGenericArguments().Select(a => a.GetTypeAnatomy()).ToArray()
                        : new TypeAnatomy[] { }
                    );
        }

        public static void Serialize<T>(this T poObject, Stream poStream) where T : class, new()
        {
            XmlSerializersRepository
                .Acquire(typeof(T))
                .Serialize(poStream, poObject);
        }

        public static void Serialize<T>(this T poObject, String psFileName) where T : class, new()
        {
            var loStream = new StreamWriter(psFileName);

            try { poObject.Serialize(loStream.BaseStream); }
            finally { loStream.Close(); }
        }

        public static Byte[] Serialize<T>(this T poObject) where T : class, new()
        {
            var loMemoryStream = new MemoryStream();
            var loStream = new StreamWriter(loMemoryStream);

            try { poObject.Serialize(loStream.BaseStream); return loMemoryStream.ToArray(); }
            finally { loStream.Close(); loMemoryStream.Close(); }
        }

        public static T Deserialize<T>(this Stream poStream) where T : class, new()
        {
            return XmlSerializersRepository
                .Acquire(typeof(T))
                .Deserialize(poStream) as T;
        }

        public static T DeserializeUsingThisAsFileName<T>(this String psFileName) where T : class, new()
        {
            var loStream = new StreamReader(psFileName);

            try { return loStream.BaseStream.Deserialize<T>(); }
            finally { loStream.Close(); }
        }

        public static T Deserialize<T>(this Byte[] poArray) where T : class, new()
        {
            var loMemoryStream = new MemoryStream(poArray);
            var loStream = new StreamReader(loMemoryStream);

            try { return loStream.BaseStream.Deserialize<T>(); }
            finally { loStream.Close(); loMemoryStream.Close(); }
        }
    }
}