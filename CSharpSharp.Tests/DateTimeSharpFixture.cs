using System;
using MbUnit.Framework;

namespace CSharpSharp.Tests
{
    /// <summary>
    /// Validates the DateTime sharp properties, methods and extension methods.
    /// </summary>
    public class DateTimeSharpFixture
    {
        /// <summary>
        /// Validates that the MillisecondsSinceUNIXTime property returns the expected value.
        /// </summary>
        [Test]
        public void MillisecondsSinceUNIXTime()
        {
            double millisecondsSinceUNIXTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            double actualMillisecondsSinceUNIXTime = DateTimeSharp.MillisecondsSinceUNIXTime;

            Assert.AreApproximatelyEqual(millisecondsSinceUNIXTime, actualMillisecondsSinceUNIXTime, (double)5, "Actual delta: {0}", actualMillisecondsSinceUNIXTime - millisecondsSinceUNIXTime);
        }

        /// <summary>
        /// Validates that the tomorrow property returns the expected date time.
        /// </summary>
        [Test]
        public void Tomorrow()
        {
            DateTime tomorrow = DateTimeSharp.Tomorrow;

            Assert.AreEqual(DateTime.Today + new TimeSpan(1, 0, 0, 0), tomorrow);
        }

        /// <summary>
        /// Validates that the yesterday property returns the expected date time.
        /// </summary>
        [Test]
        public void Yesterday()
        {
            DateTime yesterday = DateTimeSharp.Yesterday;

            Assert.AreEqual(DateTime.Today - new TimeSpan(1, 0, 0, 0), yesterday);
        }

        /// <summary>
        /// Validates that the integer.Seconds() shorthand returns a TimeSpan
        /// of the number of seconds specified by the integer.
        /// </summary>
        [Test]
        public void SecondsShorthand()
        {
            TimeSpan fiveSeconds = 5.Seconds();

            Assert.AreEqual(new TimeSpan(0, 0, 5), fiveSeconds);
        }

        /// <summary>
        /// Validates that the integer.Minutes() shorthand returns a TimeSpan
        /// of the number of minutes specified by the integer.
        /// </summary>
        [Test]
        public void MinutesShorthand()
        {
            TimeSpan fiveMinutes = 5.Minutes();

            Assert.AreEqual(new TimeSpan(0, 5, 0), fiveMinutes);
        }

        /// <summary>
        /// Validates that the integer.Hours() shorthand returns a TimeSpan
        /// of the number of hours specified by the integer.
        /// </summary>
        [Test]
        public void HoursShorthand()
        {
            TimeSpan fiveHours = 5.Hours();

            Assert.AreEqual(new TimeSpan(5, 0, 0), fiveHours);
        }

        /// <summary>
        /// Validates that the integer.Days() shorthand returns a TimeSpan
        /// of the number of days specified by the integer.
        /// </summary>
        [Test]
        public void DaysShorthand()
        {
            TimeSpan fiveDays = 5.Days();

            Assert.AreEqual(new TimeSpan(5, 0, 0, 0), fiveDays);
        }

        /// <summary>
        /// Validates that the integer.Months() shorthand returns a TimeSpan
        /// of the number of months specified by the integer.
        /// </summary>
        [Test]
        public void MonthsShorthand()
        {
            TimeSpan fiveMonths = 5.Months();

            int numberOfDays = (int)(5 * DateTimeSharp.DaysInAMonth);

            Assert.AreEqual(new TimeSpan(numberOfDays, 0, 0, 0), fiveMonths);
        }

        /// <summary>
        /// Validates that the integer.Months(true) shorthand returns a TimeSpan
        /// of the number of months specified by the integer.
        /// </summary>
        [Test]
        public void MonthsShorthandAssumption()
        {
            TimeSpan sixtyMonthsWithAssumption = 60.Months(true);
            TimeSpan sixtyMonthsWithoutAssumption = 60.Months();

            // Make sure they are not the same
            Assert.AreNotEqual(sixtyMonthsWithAssumption, sixtyMonthsWithoutAssumption);

            // Compare with a manually calculated number of days
            TimeSpan sixtyMonthsOf30Days = new TimeSpan(60 * 30, 0, 0, 0);
            Assert.AreEqual(sixtyMonthsWithAssumption, sixtyMonthsOf30Days);
        }

        /// <summary>
        /// Validates that the integer.Years() shorthand returns a TimeSpan
        /// of the number of years specified by the integer.
        /// </summary>
        [Test]
        public void YearsShorthand()
        {
            TimeSpan fiveYears = 5.Years();

            int numberOfDays = (int)(5 * DateTimeSharp.DaysInAYear);

            Assert.AreEqual(new TimeSpan(numberOfDays, 0, 0, 0), fiveYears);
        }

        /// <summary>
        /// Validates that the integer.Years(true) shorthand returns a TimeSpan
        /// of the number of months specified by the integer.
        /// </summary>
        [Test]
        public void YearsShorthandAssumption()
        {
            TimeSpan tenYearsWithAssumption = 10.Years(true);
            TimeSpan tenYearsWithoutAssumption = 10.Years();

            // Make sure they are not the same
            Assert.AreNotEqual(tenYearsWithAssumption, tenYearsWithoutAssumption);

            // Compare with a manually calculated number of days
            TimeSpan tenYearsOf365Days = new TimeSpan(10 * 365, 0, 0, 0);
            Assert.AreEqual(tenYearsWithAssumption, tenYearsOf365Days);
        }

        /// <summary>
        /// Validates that trying to set a number of months that results
        /// in an overflow raises an exception.
        /// </summary>
        [Test]
        [ExpectedException(typeof(OverflowException))]
        public void MonthsOverflow()
        {
            ((int)(int.MaxValue / DateTimeSharp.DaysInAMonth) + 1).Months();
        }

        /// <summary>
        /// Validates that trying to set a number of years that results
        /// in an overflow raises an exception.
        /// </summary>
        [Test]
        [ExpectedException(typeof(OverflowException))]
        public void YearsOverflow()
        {
            ((int)(int.MaxValue / DateTimeSharp.DaysInAYear) + 1).Years();
        }

        /// <summary>
        /// Validates that the timeSpan.Ago() shorthand returns DateTime.Now - TimeSpan.
        /// </summary>
        [Test]
        public void TimeSpanAgo()
        {
            DateTime fiveMinutesAgo = 5.Minutes().Ago();

            Assert.AreApproximatelyEqual(DateTime.Now - new TimeSpan(0, 5, 0), fiveMinutesAgo, new TimeSpan(0, 0, 1));
        }

        /// <summary>
        /// Validates that the timeSpan.FromNow() shorthand returns DateTime.Now + TimeSpan.
        /// </summary>
        [Test]
        public void TimeSpanFromNow()
        {
            DateTime threeHoursFromNow = 3.Hours().FromNow();

            Assert.AreApproximatelyEqual(DateTime.Now + new TimeSpan(3, 0, 0), threeHoursFromNow, new TimeSpan(0, 0, 1));
        }
    }
}
