using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Advisor
{
    public static class StringExtensions
    {
        public static bool NullableEquals(this string a, string b)
        {
            if (a == null && b == null)
                return true;

            if (a != null && b != null)
            {
                return a.Equals(b);
            }

            return false;
        }

        public static bool NullableEquals(this string a, string b, StringComparison comparison)
        {
            if (a == null && b == null)
                return true;

            if (a != null && b != null)
            {
                return a.Equals(b, comparison);
            }

            return false;
        }

        /// <summary>
        /// Not IsNullOrEmpty
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool HasContent(this string input)
        {
            return !input.IsNullOrEmpty();
        }

        public static bool IsNullOrEmpty(this string input)
        {
            return string.IsNullOrEmpty(input);
        }

        public static bool IsNullOrWhiteSpace(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        public static string GetContentOrDefault(this string input, string defaultValue)
        {
            return input.IsNullOrEmpty() ? defaultValue : input;
        }

        /// <summary>
        /// Returns null if string is empty or whitespace, else the string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToNullIfEmpty(this string input)
        {
            return string.IsNullOrWhiteSpace(input) ? null : input;
        }

        /// <summary>
        /// Retuns an emtpy string if null, else the string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToEmptyIfNull(this string input)
        {
            return input ?? string.Empty;
        }

        public static Guid ToGuid(this string input)
        {
            Guid output;
            Guid.TryParse(input, out output);
            return output;
        }

        public static Guid? ToGuidNullable(this string input)
        {
            Guid output;
            return Guid.TryParse(input, out output) ? output : (Guid?)null;
        }

        public static float ToFloat(this string input, float defaultValue = 0)
        {
            float output;
            if (!float.TryParse(input, out output)) output = defaultValue;
            return output;
        }

        /// <summary>
        /// convert string to int or return default value
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInt(this string input, int defaultValue)
        {
            int output;
            if (!int.TryParse(input, out output)) output = defaultValue;
            return output;
        }

        /// <summary>
        /// convert string to int or return default value
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool ToBool(this string input, bool defaultValue)
        {
            bool output;
            if (!bool.TryParse(input, out output)) output = defaultValue;
            return output;
        }

        /// <summary>
        /// convert string to long or return default value
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long ToLong(this string input, long defaultValue)
        {
            long output;
            if (!long.TryParse(input, out output)) output = defaultValue;
            return output;
        }

        /// <summary>
        /// Convert string to long?
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static long? ToLong(this string input)
        {
            long output;
            return long.TryParse(input, out output) ? output : (long?)null;
        }

        public static string Reverse(this string input)
        {
            int len = input.Length;
            char[] arr = new char[len];

            for (int i = 0; i < len; i++)
            {
                arr[i] = input[len - 1 - i];
            }
            return new string(arr);
        }

        public static bool Contains(this string original, string value, StringComparison comparisonType)
        {
            return original.IndexOf(value, comparisonType) >= 0;
        }

        public static string Replace(this string orginal, IDictionary<string, string> replacements)
        {
            if (orginal.IsNullOrEmpty() || replacements == null)
                return orginal;

            return replacements.Aggregate(orginal, (current, item) => current.Replace(item.Key, item.Value));
        }

        public static string Replace(this string original, string pattern, string replacement,
            StringComparison comparisonType)
        {
            if (original == null)
                return null;

            if (pattern.IsNullOrEmpty())
                return original;

            int lenPattern = pattern.Length;
            int idxPattern = -1;
            int idxLast = 0;

            var result = new StringBuilder();

            while (true)
            {
                idxPattern = original.IndexOf(pattern, idxPattern + 1, comparisonType);

                if (idxPattern < 0)
                {
                    result.Append(original, idxLast, original.Length - idxLast);
                    break;
                }

                result.Append(original, idxLast, idxPattern - idxLast);
                result.Append(replacement);

                idxLast = idxPattern + lenPattern;
            }

            return result.ToString();
        }
    }

    public static class EnumExtensions
    {
        public static string ToString<T>(this T enumValue)
        {
            Type type = enumValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumValue");
            }

            //Tries to find a DescriptionAttribute for a potential friendly name
            //for the enum
            MemberInfo[] memberInfo = type.GetMember(enumValue.ToString());
            if (memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs.Length > 0)
                {
                    //Pull out the description value
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            //If we have no description attribute, just return the ToString of the enum
            return enumValue.ToString();
        }
    }
}
