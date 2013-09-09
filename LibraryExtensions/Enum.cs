using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Globalization;

namespace DSE.Extensions
{
    public static class _Enum
    {
        /*public static String ToCasedString<T>(this T poItem, DSE.Enumerations.Case poCase) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            return String.Empty;
            return DSE.EnumOperations.QueryEnumTranslation(poItem, poCase);
        }*/

        public static Boolean ContainsBitwise<T>(this T[] paItems, T poConcreteItem) where T : struct, IConvertible, IComparable
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            var loConcreteItemBitForm = poConcreteItem.ToInt32(CultureInfo.InvariantCulture);

            return paItems.Cast<Int32>().Aggregate(false, (a, x) => a || (x & loConcreteItemBitForm) == loConcreteItemBitForm);
        }
    }
}