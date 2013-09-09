using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace DSE.Extensions
{
    public static class _Func
    {
        public static T TryCall<T>(this Func<T> poFunctor, T poFailsafe)
        {
            try { return poFunctor.Invoke(); }
            catch { }
            return poFailsafe;
        }

        public static T TryCall<T>(this Func<T> poFunctor, T poFailsafe, Func<Exception, T, T> poFailsafeFunctor)
        {
            var loSavedException = (Exception)null;

            try { return poFunctor.Invoke(); }
            catch (Exception loException) { loSavedException = loException; }

            return poFailsafeFunctor == null
                ? poFailsafe
                : poFailsafeFunctor(loSavedException, poFailsafe);
        }
    }
}