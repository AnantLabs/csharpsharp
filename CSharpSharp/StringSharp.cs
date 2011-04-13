using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpSharp
{
    /// <summary>
    /// Provides additional methods to help working with strings.
    /// </summary>
    public static class StringSharp
    {
        #region Casing methods

        /// <summary>
        /// Returns a copy of this <c>System.String</c> converted to camel case.
        /// The first word of an identifier is lowercase and the first letter of each subsequent concatenated word is capitalized.
        /// </summary>
        /// <param name="theString">The string.</param>
        /// <returns>A <c>System.String</c> in camelCase.</returns>
        public static string ToCamelCase(this string theString)
        {
            string camelCaseVersion = "";

            int index = 0;
            foreach (string segment in theString.SplitWords())
            {
                bool isFirstSegment = index == 0;

                if (isFirstSegment)
                {
                    camelCaseVersion += segment.ToLowerInvariant();
                }
                else
                {
                    camelCaseVersion += segment.Capitalize();
                }

                index++;
            }

            return camelCaseVersion;
        }

        /// <summary>
        /// Returns a copy of this <c>System.String</c> converted to pascal case.
        /// The first letter of each concatenated word are capitalized.
        /// </summary>
        /// <param name="theString">The string.</param>
        /// <returns>A <c>System.String</c> in PascalCase.</returns>
        public static string ToPascalCase(this string theString)
        {
            string pascalCaseVersion = "";

            foreach (string segment in theString.SplitWords())
            {
                pascalCaseVersion += segment.Capitalize();
            }

            return pascalCaseVersion;
        }

        /// <summary>
        /// Returns a copy of this <c>System.String</c> converted to title case.
        /// The first letter of each word are capitalized and all the words are separated by a space.
        /// </summary>
        /// <param name="theString">The string.</param>
        /// <returns>A <c>System.String</c> in Title Case.</returns>
        public static string ToTitleCase(this string theString)
        {
            string titleCaseVersion = "";

            foreach (string segment in theString.SplitWords())
            {
                titleCaseVersion += segment.Capitalize() + " ";
            }

            // Remove the trailing space
            if (titleCaseVersion.Length > 0)
            {
                titleCaseVersion = titleCaseVersion.Substring(0, titleCaseVersion.Length - 1);
            }

            return titleCaseVersion;
        }

        /// <summary>
        /// Returns a copy of this <c>System.String</c> converted to plain english.
        /// All the words are in lowercase and are separated by a space.
        /// </summary>
        /// <param name="theString">The string.</param>
        /// <returns>A <c>System.String</c> in plain english.</returns>
        public static string ToPlainEnglish(this string theString)
        {
            return theString.ToPlainEnglish(false);
        }

        /// <summary>
        /// Returns a copy of this <c>System.String</c> converted to plain english.
        /// All the words are in lowercase and are separated by a space. The first word
        /// will be capitalized if requested.
        /// </summary>
        /// <param name="theString">The string.</param>
        /// <param name="capitalizeFirstWord">if set to <c>true</c> capitalizes the first word.</param>
        /// <returns>A <c>System.String</c> in Plain english.</returns>
        public static string ToPlainEnglish(this string theString, bool capitalizeFirstWord)
        {
            string plainEnglishVersion = "";

            int index = 0;
            foreach (string word in theString.SplitWords())
            {
                bool isFirstWord = index == 0;

                plainEnglishVersion += (isFirstWord && capitalizeFirstWord ? word.Capitalize() : word) + " ";

                index++;
            }

            // Remove the trailing space
            if (plainEnglishVersion.Length > 0)
            {
                plainEnglishVersion = plainEnglishVersion.Substring(0, plainEnglishVersion.Length - 1);
            }

            return plainEnglishVersion;
        }

        /// <summary>
        /// Returns a copy of this <c>System.String</c> with the
        /// first letter in uppercase.
        /// </summary>
        /// <param name="theString">The string.</param>
        /// <returns>A <c>System.String</c> with the first letter in uppercase.</returns>
        public static string Capitalize(this string theString)
        {
            if (String.IsNullOrEmpty(theString))
            {
                throw new ArgumentException("Can't capitalize an empty string.");
            }

            bool hasMoreThanOneCharacter = theString.Length > 1;
            return char.ToUpperInvariant(theString[0]) + (hasMoreThanOneCharacter ? theString.Substring(1) : "");
        }

        /// <summary>
        /// Returns a string array that contains the words in the string delimited by a space or a group
        /// of capital letters (such as an acronym).
        /// </summary>
        /// <param name="theString">The string.</param>
        /// <returns>An array of <c>System.String</c> in lowercase.</returns>
        public static string[] SplitWords(this string theString)
        {
            // Add a space before every word (a word starts by a capital letter or at the first letter after a space)
            // A group of uppercase characters will be considered a word because it probably is an acronym
            // ex: VariableName -> Variable Name, IPAddress -> IP Address
            bool lastCharWasUpper = false;
            for (int index = 0; index < theString.Length; index++)
            {
                bool charIsUpper = char.IsUpper(theString, index);
                bool nextCharIsLower = index != theString.Length - 1 ? char.IsLower(theString, index + 1) : false;

                if (charIsUpper && (!lastCharWasUpper || nextCharIsLower))
                {
                    // Insert a space before the char (and update the index to stay on the char)
                    theString = theString.Insert(index, " ");
                    index++;
                }

                lastCharWasUpper = charIsUpper;
            }

            // Split the string into words
            string[] segments = theString.Split(new char[] { ' ' },  StringSplitOptions.RemoveEmptyEntries);

            // Lowercase each word (except words that are all uppercase, which are most probably acronyms) 
            for (int index = 0; index < segments.Length; index++)
            {
                bool isAllUppercase = segments[index] == segments[index].ToUpperInvariant();

                if (!isAllUppercase)
                {
                    segments[index] = segments[index].ToLowerInvariant();
                }
            }

            return segments;
        }

        /// <summary>
        /// Splits the lines.
        /// </summary>
        /// <param name="theString">The string.</param>
        /// <returns></returns>
        public static string[] SplitLines(this string theString)
        {
            return theString.SplitLines(StringSplitOptions.None);
        }

        /// <summary>
        /// Splits the lines.
        /// </summary>
        /// <param name="theString">The string.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public static string[] SplitLines(this string theString, StringSplitOptions options)
        {
            return theString.Split(new string[] { "\r\n", "\n", "\r" }, options);
        }

        #endregion // Casing methods

        #region Cardinality methods

        /// <summary>
        /// Singularizes the string.
        /// </summary>
        /// <param name="theString">The string.</param>
        /// <returns></returns>
        public static string Singularize(this string theString)
        {
            return System.Data.Entity.Design.PluralizationServices.PluralizationService.CreateService(System.Threading.Thread.CurrentThread.CurrentUICulture).Singularize(theString);
        }

        /// <summary>
        /// Pluralizes the string.
        /// </summary>
        /// <param name="theString">The string.</param>
        /// <returns></returns>
        public static string Pluralize(this string theString)
        {
            return System.Data.Entity.Design.PluralizationServices.PluralizationService.CreateService(System.Threading.Thread.CurrentThread.CurrentUICulture).Pluralize(theString);
        }

        #endregion // Cardinality methods

        #region Conversion methods

        /// <summary>
        /// Fluent interface for <c>int.Parse(value)</c>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The integer representation of the value.</returns>
        public static int ToInt(this string value)
        {
            return int.Parse(value);
        }

        /// <summary>
        /// Fluent interface for <c>long.Parse(value)</c>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The longeger representation of the value.</returns>
        public static long ToLong(this string value)
        {
            return long.Parse(value);
        }

        /// <summary>
        /// Fluent interface for <c>BooleanSharp.Parse(value)</c>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <exception cref="System.FormatException">The value is not an accepted string.</exception>
        /// <returns>true if value is accepted true string; otherwise, false.</returns>
        public static bool ToBool(this string value)
        {
            return value.ToBool(false);
        }

        /// <summary>
        /// Converts the value using either <c>BooleanSharp.Parse(value)</c> or
        /// <c>bool.Parse(value)</c> depending on the value of <c>useStandardBooleanParser</c>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="useStandardBooleanParser">if set to <c>true</c> use the standard boolean parser (<c>bool.Parse(value)</c>).</param>
        /// <returns>
        /// true if value is an accepted true string; otherwise, false.
        /// </returns>
        /// <exception cref="System.FormatException">The value is not an accepted string.</exception>
        public static bool ToBool(this string value, bool useStandardBooleanParser)
        {
            if (useStandardBooleanParser)
            {
                return bool.Parse(value);
            }
            else
            {
                return BooleanSharp.Parse(value);
            }
        }

        /// <summary>
        /// Converts the string representation of the name or numeric value of one or
        /// more enumerated constants to an equivalent enumerated object.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>
        /// An object of type TEnum whose value is represented by value.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// TEnum is not an System.Enum.
        /// -or-
        /// value is either an empty string ("") or only contains white space.
        /// -or-
        /// value is a name, but not one of the named constants defined for the enumeration.
        /// </exception>
        /// <exception cref="System.OverflowException">
        /// value is outside the range of the underlying type of TEnum.
        /// </exception>
        public static TEnum ToEnum<TEnum>(this string value)
            where TEnum : struct
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value, true);
        }

        #endregion // Conversion methods
    }
}
