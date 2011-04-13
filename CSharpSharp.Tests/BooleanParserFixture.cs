using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;

namespace CSharpSharp.Tests
{
    /// <summary>
    /// Validates the BooleanParser properties, methods and extension methods.
    /// </summary>
    public class BooleanParserFixture
    {
        /// <summary>
        /// Validates that the standard accepted strings get parsed correctly.
        /// </summary>
        /// <param name="stringValue">The string value.</param>
        /// <param name="expectedResult">if set to <c>true</c> [expected result].</param>
        [Test]
        [Row("true", true)]
        [Row("false", false)]
        [Row("tRuE", true)]
        [Row("FaLsE", false)]
        [Row("1", true)]
        [Row("0", false)]
        [Row("yes", true)]
        [Row("no", false)]
        [Row("yEs", true)]
        [Row("No", false)]
        [Row("     true   ", true)]
        [Row("  false    ", false)]
        [Row("  1   ", true)]
        [Row("    0  ", false)]
        public void StandardAcceptedStrings(string stringValue, bool expectedResult)
        {
            bool actualResult;
            bool parsingWorked = new BooleanParser().TryParse(stringValue, out actualResult);

            Assert.IsTrue(parsingWorked);
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Validates that new strings can be added for parsing.
        /// </summary>
        /// <param name="stringValue">The string value.</param>
        /// <param name="expectedResult">if set to <c>true</c> [expected result].</param>
        [Test]
        [Row("oui", true)]
        [Row("non", false)]
        [Row("asdf", true)]
        [Row("asdf", false)]
        public void CanAddAcceptedStrings(string stringValue, bool expectedResult)
        {
            BooleanParser booleanParser = new BooleanParser();

            if (expectedResult)
            {
                booleanParser.AddAcceptedTrueString(stringValue);
            }
            else
            {
                booleanParser.AddAcceptedFalseString(stringValue);
            }

            bool actualResult;
            bool parsingWorked = booleanParser.TryParse(stringValue, out actualResult);

            Assert.IsTrue(parsingWorked);
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Validates that resetting the accepted strings removes
        /// the added accepted strings from the accepted strings.
        /// </summary>
        [Test]
        public void ResetAcceptedStringsTest()
        {
            BooleanParser booleanParser = new BooleanParser();

            booleanParser.AddAcceptedTrueString("oui");
            booleanParser.AddAcceptedFalseString("non");

            // We already know from another test that those strings will parse

            booleanParser.ResetAcceptedStrings();

            bool parseResult;
            bool trueStringParsingWorked = booleanParser.TryParse("oui", out parseResult);
            bool falseStringParsingWorked = booleanParser.TryParse("non", out parseResult);

            Assert.IsFalse(trueStringParsingWorked);
            Assert.IsFalse(falseStringParsingWorked);
        }

        /// <summary>
        /// Validates that the collection of accepted strings can be seen.
        /// </summary>
        [Test]
        public void CanSeeCollectionOfAcceptedStrings()
        {
            BooleanParser booleanParser = new BooleanParser();

            Assert.GreaterThanOrEqualTo(1, booleanParser.AcceptedTrueStrings.Count());
            Assert.GreaterThanOrEqualTo(1, booleanParser.AcceptedFalseStrings.Count());
        }
    }
}
