using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpSharp
{
    /// <summary>
    /// Provides additional methods for working with DateTime objects.
    /// </summary>
    public static class DateTimeSharp
    {
        /// <summary>
        /// The average number of days in a month.
        /// </summary>
        public const decimal DaysInAMonth = 30.4368499m;

        /// <summary>
        /// The average number of days in a year.
        /// </summary>
        public const decimal DaysInAYear = 365.242199m;

        /// <summary>
        /// Gets the number of milliseconds ellapsed since the start of UNIX time
        /// which started January 1st, 1970. This is the same baseline as
        /// Javascript's getTime() method making this useful for calculating
        /// timings.
        /// </summary>
        public static double MillisecondsSinceUNIXTime
        {
            get
            {
                return (long)((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
            }
        }

        /// <summary>
        /// Gets a datetime for tomorrow.
        /// </summary>
        public static DateTime Tomorrow
        {
            get
            {
                return DateTime.Today.AddDays(1);
            }
        }

        /// <summary>
        /// Gets a datetime for yesterday.
        /// </summary>
        public static DateTime Yesterday
        {
            get
            {
                return DateTime.Today.AddDays(-1);
            }
        }

        #region Integer shorthands

        /// <summary>
        /// Returns a time span with the specified number of seconds.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A time span with the specified number of seconds.</returns>
        public static TimeSpan Seconds(this int value)
        {
            return new TimeSpan(0, 0, value);
        }

        /// <summary>
        /// Returns a time span with the specified number of minutes.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A time span with the specified number of minutes.</returns>
        public static TimeSpan Minutes(this int value)
        {
            return new TimeSpan(0, value, 0);
        }

        /// <summary>
        /// Returns a time span with the specified number of hours.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A time span with the specified number of hours.</returns>
        public static TimeSpan Hours(this int value)
        {
            return new TimeSpan(value, 0, 0);
        }

        /// <summary>
        /// Returns a time span with the specified number of days.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A time span with the specified number of days.</returns>
        public static TimeSpan Days(this int value)
        {
            return new TimeSpan(value, 0, 0, 0);
        }

        /// <summary>
        /// Returns a time span with the specified number of months.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A time span with the specified number of monts.</returns>
        public static TimeSpan Months(this int value)
        {
            return value.Months(false);
        }

        /// <summary>
        /// Returns a time span with the specified number of months.
        /// The average number of days in a month is used, if you want
        /// to assume 30 days per month, call the other method.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="assume30DaysPerMonth">
        /// Flag indicating whether to assume that a month has 30 days or to use the
        /// average number of days per month to calculate the time span.
        /// </param>
        /// <returns>
        /// A time span with the specified number of monts.
        /// </returns>
        public static TimeSpan Months(this int value, bool assume30DaysPerMonth)
        {
            try
            {
                decimal daysPerMonth = assume30DaysPerMonth ? 30 : DaysInAMonth;

                return new TimeSpan((int)Math.Round(value * daysPerMonth), 0, 0, 0);
            }
            catch (OverflowException ex)
            {
                const int MaximumNumberOfMonths = (int)(int.MaxValue / DaysInAMonth);
                throw new OverflowException("The maximum number of months is " + MaximumNumberOfMonths, ex);
            }
        }

        /// <summary>
        /// Returns a time span with the specified number of years.
        /// The average number of days in a year is used, if you want
        /// to assume 365 days per year, call the other method.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// A time span with the specified number of years.
        /// </returns>
        public static TimeSpan Years(this int value)
        {
            return value.Years(false);
        }

        /// <summary>
        /// Returns a time span with the specified number of years.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="assume365DaysPerYear">
        /// Flag indicating whether to assume that a year has 365 days or to use the
        /// average number of days per year to calculate the time span.
        /// </param>
        /// <returns>
        /// A time span with the specified number of years.
        /// </returns>
        public static TimeSpan Years(this int value, bool assume365DaysPerYear)
        {
            try
            {
                decimal daysPerYear = assume365DaysPerYear ? 365 : DaysInAYear;

                return new TimeSpan((int)Math.Round(value * daysPerYear), 0, 0, 0);
            }
            catch (OverflowException ex)
            {
                const int MaximumNumberOfYears = (int)(int.MaxValue / DaysInAYear);
                throw new OverflowException("The maximum number of years is " + MaximumNumberOfYears, ex);
            }
        }

        #endregion // Integer shorthands

        /// <summary>
        /// Returns a datetime in the past, set at today minus the timespan.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime Ago(this TimeSpan value)
        {
            return DateTime.Now.Subtract(value);
        }

        /// <summary>
        /// Returns a datetime in the future, set at today plus the timespan.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime FromNow(this TimeSpan value)
        {
            return DateTime.Now.Add(value);
        }
    }
}
