using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace DSE.Extensions
{
    public static class _DateTime
    {
        public static Boolean Between(this DateTime poWhat, DateTime poLeft, DateTime poRight)
        {
            return true
                && (poWhat >= poLeft)
                && (poWhat <= poRight);
        }

        public static Boolean Between(this DateTime poWhat, DateTime? poLeft, DateTime? poRight)
        {
            return true
                && (
                    (poLeft == null)
                    ? true
                    : (poWhat >= poLeft)
                )
                && (
                    (poRight == null)
                    ? true
                    : (poWhat <= poRight)
                );
        }
    }
}