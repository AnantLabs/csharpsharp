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
        /// The default boolean parser.
        /// </summary>
        private static BooleanParser booleanParser;

        /// <summary>
        /// Gets the collection of strings accepted as meaning "true".
        /// </summary>
        /// <value>The accepted true strings.</value>
        public static IEnumerable<string> AcceptedTrueStrings
        {
            get
            {
                lock (ThreadSafetyLock)
                {
                    return booleanParser.AcceptedTrueStrings;
                }
            }
        }

        /// <summary>
        /// Gets the collection of strings accepted as meaning "false".
        /// </summary>
        /// <value>The accepted false strings.</value>
        public static IEnumerable<string> AcceptedFalseStrings
        {
            get
            {
                lock (ThreadSafetyLock)
                {
                    return booleanParser.AcceptedFalseStrings;
                }
            }
        }

        /// <summary>
        /// Initializes the <see cref="BooleanSharp"/> class.
        /// </summary>
        static BooleanSharp()
        {
            booleanParser = new BooleanParser();
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public static void ResetAcceptedStrings()
        {
            lock (ThreadSafetyLock)
            {
                booleanParser.ResetAcceptedStrings();
            }
        }

        /// <summary>
        /// Clears the accepted true strings.
        /// </summary>
        public static void ClearAcceptedTrueStrings()
        {
            lock (ThreadSafetyLock)
            {
                booleanParser.ClearAcceptedTrueStrings();
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
                booleanParser.AddAcceptedTrueString(trueString);
            }
        }

        /// <summary>
        /// Clears the accepted false strings.
        /// </summary>
        public static void ClearAcceptedFalseStrings()
        {
            lock (ThreadSafetyLock)
            {
                booleanParser.ClearAcceptedFalseStrings();
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
                booleanParser.AddAcceptedFalseString(falseString);
            }
        }
        
        #endregion // Class setup

        /// <summary>
        /// Converts the specified string representation of a logical value to its System.Boolean equivalent.
        /// </summary>
        /// <param name="value">A string containing the value to convert.</param>
        /// <exception cref="System.ArgumentNullException">The value is null.</exception>
        /// <exception cref="System.FormatException">The value is not an accepted string.</exception>
        /// <returns>true if value is equivalent to System.Boolean.TrueString; otherwise, false.</returns>
        public static bool Parse(string value)
        {
            lock (ThreadSafetyLock)
            {
                return booleanParser.Parse(value);
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
        public static bool TryParse(string value, out bool result)
        {
            lock (ThreadSafetyLock)
            {
                return booleanParser.TryParse(value, out result);
            }
        }
    }
}
