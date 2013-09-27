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
    public static class _String
    {
        public static String ToTitleCase(this String poString)
        {
            return
                String.IsNullOrEmpty(poString)
                    ? poString
                    : System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(poString.ToLower());
        }

        public static String ToFirstLetterUpperCase(this String poString)
        {
            if (!String.IsNullOrEmpty(poString))
            {
                if (poString.Length > 0)
                {
                    var loStringBuilder = new StringBuilder();
                    loStringBuilder.Append(char.ToUpper(poString[0]) + poString.Substring(1, poString.Length - 1));

                    return loStringBuilder.ToString(); ;
                }
            }

            return poString;
        }

        public static String GetFirstLetter(this String poString)
        {
            return
                String.IsNullOrEmpty(poString)
                    ? poString
                    : poString[0].ToString();
        }

        public static String GetFirstLetterSuffixedIfNotEmpty(this String poString, String psAppendix)
        {
            var loResult = poString.GetFirstLetter();

            return
                String.IsNullOrEmpty(loResult)
                    ? loResult
                    : loResult + psAppendix;
        }

        private static Regex _oHTMLRegex = new Regex("<.*?>", RegexOptions.Compiled);

        public static String StripHTMLTags(this String poSource)
        {
            return String.IsNullOrEmpty(poSource)
                ? poSource
                : _oHTMLRegex.Replace(poSource, String.Empty);
        }

        public static String Join(this String[] paStrings, String psSeparator)
        {
            try { return String.Join(psSeparator, paStrings); }
            catch (ArgumentNullException ex) { throw new NullReferenceException("", ex); }
        }

        public static String JoinIf(this String[] paStrings, Func<String, bool> poPredicate, String psSeparator)
        {
            try { return String.Join(psSeparator, paStrings.Where(poPredicate).ToArray()); }
            catch (ArgumentNullException ex) { throw new NullReferenceException("", ex); }
        }

        public static String JoinNotNullOrEmpty(this String[] paStrings, String psSeparator)
        {
            return paStrings.JoinIf(s => !String.IsNullOrEmpty(s), psSeparator);
        }

        public static String ToEncoding(this String psValue, Encoding poDestinationEncoding)
        {
            return psValue.ToEncoding(Encoding.UTF8, poDestinationEncoding);
        }

        public static String ToEncoding(this String psValue, Encoding poSourceEncoding, Encoding poDestinationEncoding)
        {
            if (String.IsNullOrEmpty(psValue))
            {
                return psValue;
            }

            var loSourceBytes = poSourceEncoding.GetBytes(psValue);
            var loDestinationBytes = Encoding.Convert(poSourceEncoding, poDestinationEncoding, loSourceBytes);
            var loDestinationChars = new char[poDestinationEncoding.GetCharCount(loDestinationBytes, 0, loDestinationBytes.Length)];

            poDestinationEncoding.GetChars(loDestinationBytes, 0, loDestinationChars.Length, loDestinationChars, 0);

            return new string(loDestinationChars);
        }
    }
}