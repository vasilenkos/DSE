using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace DSE.Extensions
{
    public static class _Dictionary
    {
        public static P TryGetValue<T, P>(this Dictionary<T, P> poDictionary, T poKey, P poDefault, Func<P, P> poFunction)
        {
            if (poDictionary == null)
            {
                return poDefault;
            }

            try { return poFunction(poDictionary[poKey]); }
            catch (KeyNotFoundException) { return poDefault; }
        }

        public static P TryGetValue<T, P>(this Dictionary<T, P> poDictionary, T poKey, P poDefault)
        {
            if (poDictionary == null)
            {
                return poDefault;
            }

            try { return poDictionary[poKey]; }
            catch (KeyNotFoundException) { return poDefault; }
        }
    }
}