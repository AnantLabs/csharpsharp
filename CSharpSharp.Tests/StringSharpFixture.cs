using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;

namespace CSharpSharp.Tests
{
    /// <summary>
    /// Validates the StringSharp properties, methods and extension methods.
    /// </summary>
    public class StringSharpFixture
    {
        /// <summary>
        /// Validates that the camel casing method returns a string
        /// in the appropriate format.
        /// </summary>
        /// <param name="normalString">The normal string.</param>
        /// <param name="plainEnglishString">The camel cased string.</param>
        [Test]
        [Row("my variable", "myVariable")]
        [Row("MyVariable", "myVariable")]
        [Row("My Variable", "myVariable")]
        [Row("the IP Adress", "theIPAdress")]
        [Row("TheIPAdress", "theIPAdress")]
        [Row("IP Adress", "ipAdress")]
        [Row("IP", "ip")]
        public void CamelCasingTest(string normalString, string camelCasedString)
        {
            Assert.AreEqual(camelCasedString, normalString.ToCamelCase());
        }
        /// <summary>
        /// Validates that the pascal casing method returns a string
        /// in the appropriate format.
        /// </summary>
        /// <param name="normalString">The normal string.</param>
        /// <param name="pascalCasedString">The pascal cased string.</param>
        [Test]
        [Row("my variable", "MyVariable")]
        [Row("MyVariable", "MyVariable")]
        [Row("My Variable", "MyVariable")]
        [Row("the IP Adress", "TheIPAdress")]
        [Row("TheIPAdress", "TheIPAdress")]
        [Row("IP Adress", "IPAdress")]
        [Row("IP", "IP")]
        public void PascalCasingTest(string normalString, string pascalCasedString)
        {
            Assert.AreEqual(pascalCasedString, normalString.ToPascalCase());
        }

        /// <summary>
        /// Validates that the title casing method returns a string
        /// in the appropriate format.
        /// </summary>
        /// <param name="normalString">The normal string.</param>
        /// <param name="titleCasedString">The title cased string.</param>
        [Test]
        [Row("my variable", "My Variable")]
        [Row("MyVariable", "My Variable")]
        [Row("My Variable", "My Variable")]
        [Row("the IP Adress", "The IP Adress")]
        [Row("TheIPAdress", "The IP Adress")]
        [Row("IP Adress", "IP Adress")]
        [Row("IP", "IP")]
        public void TitleCasingTest(string normalString, string titleCasedString)
        {
            Assert.AreEqual(titleCasedString, normalString.ToTitleCase());
        }

        /// <summary>
        /// Validates that the plain english conversion method returns a string
        /// in the appropriate format.
        /// </summary>
        /// <param name="normalString">The normal string.</param>
        /// <param name="plainEnglishString">The plain english string.</param>
        [Test]
        [Row("my variable", "my variable")]
        [Row("MyVariable", "my variable")]
        [Row("My Variable", "my variable")]
        [Row("the IP Adress", "the IP adress")]
        [Row("TheIPAdress", "the IP adress")]
        [Row("IP Adress", "IP adress")]
        [Row("IP", "IP")]
        public void PlainEnglishConversionTest(string normalString, string plainEnglishString)
        {
            Assert.AreEqual(plainEnglishString, normalString.ToPlainEnglish());
        }

        /// <summary>
        /// Validates that trying to capitalize an empty string raises an exception.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CapitalizeEmptyString()
        {
            "".Capitalize();
        }

        /// <summary>
        /// Validates that the split line method splits on any kind of new line character.
        /// </summary>
        [Test]
        [Row("\n")]
        [Row("\r")]
        [Row("\r\n")]
        public void SplitLinesTest(string newLineCharacter)
        {
            string stringToSplit = "asdf" + newLineCharacter + "jkl;" + newLineCharacter + "qwerty";

            Assert.AreEqual(3, stringToSplit.SplitLines().Length);
        }

        /// <summary>
        /// Validates that the Singularize method properly converts
        /// a string to its singular form.
        /// </summary>
        /// <param name="stringToSingularize">The string to singularize.</param>
        /// <param name="expectedString">The expected string.</param>
        [Test]
        [Row("things", "thing")]
        [Row("thing", "thing")]
        [Row("theThings", "theThing")]
        public void SingularizationTest(string stringToSingularize, string expectedString)
        {
            Assert.AreEqual(expectedString, stringToSingularize.Singularize());
        }

        /// <summary>
        /// Validates that the Pluralize method properly converts
        /// a string to its plurar form.
        /// </summary>
        /// <param name="stringToPluralize">The string to plurarize.</param>
        /// <param name="expectedString">The expected string.</param>
        [Test]
        [Row("thing", "things")]
        [Row("things", "things")]
        [Row("theThing", "theThings")]
        [Row("octopus", "octopuses")] // not "octopi": http://en.wikipedia.org/wiki/Plural_form_of_words_ending_in_-us#Octopus
        public void PlurarizationTest(string stringToPluralize, string expectedString)
        {
            Assert.AreEqual(expectedString, stringToPluralize.Pluralize());
        }

        #region Enum conversion

        /// <summary>
        /// Gets or sets the dummy enum.
        /// </summary>
        /// <value>
        /// The dummy enum.
        /// </value>
        public enum DummyEnum : int
        {
            Option1 = 1,

            Option2 = 2,

            Option3 = 3,
        }

        /// <summary>
        /// Validates that the enum conversions converts
        /// properly.
        /// </summary>
        /// <param name="stringValue">The string value.</param>
        /// <param name="expectedEnum">The expected enum.</param>
        [Test]
        [Row("1", DummyEnum.Option1)]
        [Row("Option1", DummyEnum.Option1)]
        [Row("3", DummyEnum.Option3)]
        [Row("Option3", DummyEnum.Option3)]
        public void EnumConversionTest(string stringValue, DummyEnum expectedEnum)
        {
            Assert.AreEqual(expectedEnum, stringValue.ToEnum<DummyEnum>());
        }

        /// <summary>
        /// Validates that the enum conversion throws a System.ArgumentException
        /// when given values that cannot be parsed.
        /// </summary>
        /// <param name="stringValue">The string value.</param>
        [Test]
        [Row("")]
        [Row(null)]
        [Row("  ")]
        [Row("InexistantOption")]
        [ExpectedException(typeof(System.ArgumentException))]
        public void EnumConversionThrowsSystemArgumentException(string stringValue)
        {
            stringValue.ToEnum<DummyEnum>();
        }

        /// <summary>
        /// Validates that the enum conversion throws a System.ArgumentException
        /// when using the method to try to convert a string to a non-enum type.
        /// </summary>
        [Test]
        [ExpectedException(typeof(System.ArgumentException))]
        public void EnumConversionFailsIfEnumTypeIsNotAnEnum()
        {
            "5".ToEnum<int>();
        }

        /// <summary>
        /// Validates that the enum conversion throws a System.OverflowException
        /// when given a value outside the range of the underlying type of the enum.
        /// </summary>
        [Test]
        [ExpectedException(typeof(System.OverflowException))]
        public void EnumConversionThrowsOverflowException()
        {
            long overflowingValue = (long)int.MaxValue + 1;

            overflowingValue.ToString().ToEnum<DummyEnum>();
        }

        #endregion // Enum conversion
    }
}
