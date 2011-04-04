using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace CSharpSharp
{
    /// <summary>
    /// Provides additional methods for working with boolean values.
    /// </summary>
    public static class BooleanSharp
    {
        #region Class setup

        /// <summary>
        /// Object used to ensure the thread safety of the class.
        /// </summary>
        private static object ThreadSafetyLock = new object();

        /// <summary>
        /// The list of strings accepted as meaning "true".
        /// </summary>
        private static List<string> acceptedTrueStrings = new List<string>();

        /// <summary>
        /// Gets the collection of strings accepted as meaning "true".
        /// </summary>
        /// <value>The accepted true strings.</value>
        public static ReadOnlyCollection<string> AcceptedTrueStrings
        {
            get
            {
                lock (ThreadSafetyLock)
                {
                    return acceptedTrueStrings.AsReadOnly();
                }
            }
        }

        /// <summary>
        /// The list of strings accepted as meaning "false".
        /// </summary>
        private static List<string> acceptedFalseStrings = new List<string>();

        /// <summary>
        /// Gets the collection of strings accepted as meaning "false".
        /// </summary>
        /// <value>The accepted false strings.</value>
        public static ReadOnlyCollection<string> AcceptedFalseStrings
        {
            get
            {
                lock (ThreadSafetyLock)
                {
                    return acceptedFalseStrings.AsReadOnly();
                }
            }
        }

        /// <summary>
        /// Initializes the <see cref="BooleanSharp"/> class.
        /// </summary>
        static BooleanSharp()
        {
            ResetAcceptedStrings();
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public static void ResetAcceptedStrings()
        {
            lock (ThreadSafetyLock)
            {
                ClearAcceptedTrueStrings();
                ClearAcceptedFalseStrings();

                AddAcceptedTrueString("yes");
                AddAcceptedFalseString("no");
            }
        }

        /// <summary>
        /// Clears the accepted true strings.
        /// </summary>
        public static void ClearAcceptedTrueStrings()
        {
            lock (ThreadSafetyLock)
            {
                acceptedTrueStrings = new List<string>();
            }
        }

        /// <summary>
        /// Adds the accepted true string.
        /// </summary>
        /// <param name="trueString">The true string.</param>
        public static void AddAcceptedTrueString(string trueString)
        {
            lock(ThreadSafetyLock)
            {
                string cleanedUpString = trueString.StandardizeForComparison();

                // Don't add duplicates
                if (!acceptedTrueStrings.Contains(cleanedUpString))
                {
                    acceptedTrueStrings.Add(cleanedUpString);
                }
            }
        }

        /// <summary>
        /// Clears the accepted false strings.
        /// </summary>
        public static void ClearAcceptedFalseStrings()
        {
            lock (ThreadSafetyLock)
            {
                acceptedFalseStrings = new List<string>();
            }
        }

        /// <summary>
        /// Adds the accepted false string.
        /// </summary>
        /// <param name="falseString">The false string.</param>
        public static void AddAcceptedFalseString(string falseString)
        {
            lock (ThreadSafetyLock)
            {
                string cleanedUpString = falseString.StandardizeForComparison();

                // Don't add duplicates
                if (!acceptedFalseStrings.Contains(cleanedUpString))
                {
                    acceptedFalseStrings.Add(cleanedUpString);
                }
            }
        }

        /// <summary>
        /// Apply a few operations to the string to make it follow
        /// a standard to simplify comparisons.
        /// </summary>
        /// <param name="stringToCleanUp">The string to clean up.</param>
        /// <returns>The standardized string.</returns>
        private static string StandardizeForComparison(this string stringToCleanUp)
        {
            return stringToCleanUp.ToLowerInvariant().Trim();
        }

        #endregion // Class setup

        /// <summary>
        /// Converts the specified string representation of a logical value to its System.Boolean
        ///   equivalent. A return value indicates whether the conversion succeeded or
        ///   failed.
        /// </summary>
        /// <param name="value">A string containing the value to convert.</param>
        /// <param name="result">
        ///     When this method returns, if the conversion succeeded, contains true if value
        ///     is equivalent to System.Boolean.TrueString or false if value is equivalent
        ///     to System.Boolean.FalseString. If the conversion failed, contains false.
        ///     The conversion fails if value is null or is not equivalent to either System.Boolean.TrueString
        ///     or System.Boolean.FalseString. This parameter is passed uninitialized.
        /// </param>
        /// <returns>true if value was converted successfully; otherwise, false.</returns>
        public static bool TryParse(string value, out bool result)
        {
            // First, try the standard TryParse method
            if (Boolean.TryParse(value, out result))
            {
                return true;
            }

            // Second, compare with 1 or 0
            int integerValue;
            if (Int32.TryParse(value, out integerValue))
            {
                if (integerValue == 1 || integerValue == 0)
                {
                    result = integerValue == 1;
                    return true;
                }
            }

            // Finally, compare with the true and false equivalent strings (yes, no and any other accepted values)
            string cleanedUpValue = value.StandardizeForComparison();
            bool meansTrue = acceptedTrueStrings.Contains(cleanedUpValue);
            bool meansFalse = acceptedFalseStrings.Contains(cleanedUpValue);

            if (meansTrue || meansFalse)
            {
                result = meansTrue;
                return true;
            }

            // Could not parse the value
            return false;
        }
    }
}
