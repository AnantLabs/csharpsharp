using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpSharp
{
    /// <summary>
    /// Represents a class used for parsing boolean values that can be
    /// modified to accept strings other than true, false, 1 and 0.
    /// </summary>
    public class BooleanParser
    {
        #region Class setup

        /// <summary>
        /// The collection of strings accepted as meaning "true".
        /// </summary>
        private HashSet<string> acceptedTrueStrings = new HashSet<string>();

        /// <summary>
        /// Gets the collection of strings accepted as meaning "true".
        /// </summary>
        /// <value>The accepted true strings.</value>
        public IEnumerable<string> AcceptedTrueStrings
        {
            get { return acceptedTrueStrings; }
        }

        /// <summary>
        /// The collection of strings accepted as meaning "false".
        /// </summary>
        private HashSet<string> acceptedFalseStrings = new HashSet<string>();

        /// <summary>
        /// Gets the collection of strings accepted as meaning "false".
        /// </summary>
        /// <value>The accepted false strings.</value>
        public IEnumerable<string> AcceptedFalseStrings
        {
            get { return acceptedFalseStrings; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanParser"/> class.
        /// </summary>
        public BooleanParser()
        {
            ResetAcceptedStrings();
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void ResetAcceptedStrings()
        {
            ClearAcceptedTrueStrings();
            ClearAcceptedFalseStrings();

            AddAcceptedTrueString("yes");
            AddAcceptedFalseString("no");
        }

        /// <summary>
        /// Clears the accepted true strings.
        /// </summary>
        public void ClearAcceptedTrueStrings()
        {
            acceptedTrueStrings = new HashSet<string>();
        }

        /// <summary>
        /// Adds the accepted true string.
        /// </summary>
        /// <param name="trueString">The true string.</param>
        public void AddAcceptedTrueString(string trueString)
        {
            string cleanedUpString = StandardizeStringForComparison(trueString);

            // Don't add duplicates
            if (!acceptedTrueStrings.Contains(cleanedUpString))
            {
                acceptedTrueStrings.Add(cleanedUpString);
            }
        }

        /// <summary>
        /// Clears the accepted false strings.
        /// </summary>
        public void ClearAcceptedFalseStrings()
        {
            acceptedFalseStrings = new HashSet<string>();
        }

        /// <summary>
        /// Adds the accepted false string.
        /// </summary>
        /// <param name="falseString">The false string.</param>
        public void AddAcceptedFalseString(string falseString)
        {
            string cleanedUpString = StandardizeStringForComparison(falseString);

            // Don't add duplicates
            if (!acceptedFalseStrings.Contains(cleanedUpString))
            {
                acceptedFalseStrings.Add(cleanedUpString);
            }
        }

        /// <summary>
        /// Apply a few operations to the string to make it follow
        /// a standard to simplify comparisons.
        /// </summary>
        /// <param name="stringToCleanUp">The string to clean up.</param>
        /// <returns>The standardized string.</returns>
        internal string StandardizeStringForComparison(string stringToCleanUp)
        {
            return stringToCleanUp.ToLowerInvariant().Trim();
        }

        #endregion // Class setup

        /// <summary>
        /// Converts the specified string representation of a logical value to its System.Boolean equivalent.
        /// </summary>
        /// <param name="value">A string containing the value to convert.</param>
        /// <exception cref="System.ArgumentNullException">The value is null.</exception>
        /// <exception cref="System.FormatException">The value is not an accepted string.</exception>
        /// <returns>true if value is equivalent to System.Boolean.TrueString; otherwise, false.</returns>
        public bool Parse(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            bool parsedValue;
            if (TryParse(value, out parsedValue))
            {
                return parsedValue;
            }
            else
            {
                throw new FormatException("The value \"" + value + "\" is not an accepted string.");
            }
        }

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
        public bool TryParse(string value, out bool result)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

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
            string cleanedUpValue = StandardizeStringForComparison(value);
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
