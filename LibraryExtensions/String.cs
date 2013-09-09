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
            return poString == null
                ? poString
                : System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(poString.ToLower());
        }

        public static String ToFirstLetterUpperCase(this String poString)
        {
            if (String.IsNullOrEmpty(poString))
            {
                return poString;
            }

            if (poString.Length > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(char.ToUpper(poString[0]) + poString.Substring(1, poString.Length - 1));
                return sb.ToString(); ;
            }

            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(poString.ToLower());
        }

        public static String GetFirstLetter(this String poString)
        {
            if (String.IsNullOrEmpty(poString))
            {
                return poString;
            }

            return poString[0].ToString();
        }

        public static String GetFirstLetterAndAppendIfNotEmpty(this String poString, String psAppendix)
        {
            var loResult = poString.GetFirstLetter();

            if (!String.IsNullOrEmpty(loResult))
            {
                return loResult + psAppendix;
            }

            return loResult;
        }

        private static Regex _oHTMLRegex = new Regex("<.*?>", RegexOptions.Compiled);
        public static String StripHTMLTags(this String poSource)
        {
            return _oHTMLRegex.Replace(poSource, String.Empty);
        }

        public static String Join(this String[] paStrings, String psSeparator)
        {
            return String.Join(psSeparator, paStrings);
        }

        public static String JoinIf(this String[] paStrings, Func<String, bool> poPredicate, String psSeparator)
        {
            return String.Join(psSeparator, paStrings.Where(poPredicate).ToArray());
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

        public static String ToNameAndSurnameCase(this String poString)
        {
            if (String.IsNullOrEmpty(poString))
            {
                return poString;
            }
            else
            {
                char[] chars = poString.Trim().ToCharArray();
                char LastCharacter = ' ';
                String ResValue = "";
                Boolean NeedUpper = true;
                foreach (char character in chars)
                {
                    if (!(((character == ' ') || (character == '-')) && (character == LastCharacter)))
                    {
                        ResValue += Char.IsLower(character) ? (NeedUpper ? character.ToString().ToUpper() : character.ToString()) : (NeedUpper ? character.ToString() : character.ToString().ToLower());
                    }
                    if ((character == ' ') || (character == '-'))
                        NeedUpper = true;
                    else NeedUpper = false;
                    LastCharacter = character;
                }
                return ResValue;
            }
        }

        public static String ToPatronymicCase(this String poString)
        {
            if (String.IsNullOrEmpty(poString))
            {
                return null;
            }
            else
            {
                char[] chars = poString.Trim().ToCharArray();
                char LastCharacter = ' ';
                int i = 0;
                String ResValue = "";
                Boolean NeedUpper = true;
                foreach (char character in chars)
                {
                    if ((character == 'о') || (character == 'О'))
                    {
                        if (chars.Length - 4 >= i)
                        {
                            if (((chars[i + 1] == 'г') || (chars[i + 1] == 'Г')) &&
                                ((chars[i + 2] == 'л') || (chars[i + 2] == 'Л')) &&
                                ((chars[i + 3] == 'ы') || (chars[i + 3] == 'Ы')))
                            {
                                NeedUpper = false;
                            }
                        }
                    }
                    if ((character == 'к') || (character == 'К'))
                    {
                        if (chars.Length - 4 >= i)
                        {
                            if (((chars[i + 1] == 'ы') || (chars[i + 1] == 'Ы')) &&
                                ((chars[i + 2] == 'з') || (chars[i + 2] == 'З')) &&
                                ((chars[i + 3] == 'ы') || (chars[i + 3] == 'Ы')))
                            {
                                NeedUpper = false;
                            }
                        }
                    }
                    if (!(((character == ' ') || (character == '-')) && (character == LastCharacter)))
                    {
                        ResValue += Char.IsLower(character) ? (NeedUpper ? character.ToString().ToUpper() : character.ToString()) : (NeedUpper ? character.ToString() : character.ToString().ToLower());
                    }
                    if ((character == ' ') || (character == '-'))
                        NeedUpper = true;
                    else NeedUpper = false;
                    LastCharacter = character;
                    i++;
                }
                return ResValue;
            }
        }

    }
}