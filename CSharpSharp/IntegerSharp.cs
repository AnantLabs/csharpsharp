using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpSharp
{
    /// <summary>
    /// Provides additional methods for working with integer (or long) values.
    /// </summary>
    public static class IntegerSharp
    {
        /// <summary>
        /// Iterates between integers from start to end and
        /// incrementing by the step at each iteration.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="step">The step.</param>
        /// <returns>An enumeration of all the steps from start to end.</returns>
        public static IEnumerable<int> To(this int start, int end, int step = 1)
        {
            int currentIteration = start;

            while (currentIteration <= end)
            {
                yield return currentIteration;

                currentIteration += step;
            }
        }

        /// <summary>
        /// Iterates between longs from start to end and
        /// incrementing by the step at each iteration.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="step">The step.</param>
        /// <returns>An enumeration of all the steps from start to end.</returns>
        public static IEnumerable<long> To(this long start, long end, long step = 1)
        {
            long currentIteration = start;

            while (currentIteration <= end)
            {
                yield return currentIteration;

                currentIteration += step;
            }
        }
    }
}
