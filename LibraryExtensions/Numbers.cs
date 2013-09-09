using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace DSE.Extensions
{
    public static class _Numbers
    {
        #region Between
        public static Boolean Between(this decimal pnValue, decimal pnLeft, decimal pnRight)
        {
            return (pnValue >= pnLeft) && (pnValue <= pnRight);
        }

        public static Boolean Between(this int pnValue, int pnLeft, int pnRight)
        {
            return (pnValue >= pnLeft) && (pnValue <= pnRight);
        }

        public static Boolean Between(this double pdValue, double pdLeft, double pdRight)
        {
            return (pdValue >= pdLeft) && (pdValue <= pdRight);
        }

        public static Boolean Between(this float pfValue, float pfLeft, float pfRight)
        {
            return (pfValue >= pfLeft) && (pfValue <= pfRight);
        }
        #endregion

        #region ClampWithRange
        public static decimal ClampWithRange(this decimal pnValue, decimal pnLeft, decimal pnRight)
        {
            if (pnValue < pnLeft) { return pnLeft; }
            if (pnValue > pnRight) { return pnRight; }
            return pnValue;
        }

        public static int ClampWithRange(this int pnValue, int pnLeft, int pnRight)
        {
            if (pnValue < pnLeft) { return pnLeft; }
            if (pnValue > pnRight) { return pnRight; }
            return pnValue;
        }

        public static double ClampWithRange(this double pdValue, double pdLeft, double pdRight)
        {
            if (pdValue < pdLeft) { return pdLeft; }
            if (pdValue > pdRight) { return pdRight; }
            return pdValue;
        }

        public static float ClampWithRange(this float pfValue, float pfLeft, float pfRight)
        {
            if (pfValue < pfLeft) { return pfLeft; }
            if (pfValue > pfRight) { return pfRight; }
            return pfValue;
        }
        #endregion

        #region ClampWithRange IEnumerable<...> Mode
        public static IEnumerable<decimal> ClampWithRange(this IEnumerable<decimal> poSource, decimal pnLeft, decimal pnRight)
        {
            foreach (var n in poSource)
            {
                yield return n.ClampWithRange(pnLeft, pnRight);
            }
        }

        public static IEnumerable<int> ClampWithRange(this IEnumerable<int> poSource, int pnLeft, int pnRight)
        {
            foreach (var n in poSource)
            {
                yield return n.ClampWithRange(pnLeft, pnRight);
            }
        }

        public static IEnumerable<double> ClampWithRange(this IEnumerable<double> poSource, double pdLeft, double pdRight)
        {
            foreach (var n in poSource)
            {
                yield return n.ClampWithRange(pdLeft, pdRight);
            }
        }

        public static IEnumerable<float> ClampWithRange(this IEnumerable<float> poSource, float pfLeft, float pfRight)
        {
            foreach (var n in poSource)
            {
                yield return n.ClampWithRange(pfLeft, pfRight);
            }
        }
        #endregion
    }
}