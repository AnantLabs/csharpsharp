using System;
using MbUnit.Framework;

namespace CSharpSharp.Tests
{
    /// <summary>
    /// Validates the IntegerSharp properties, methods and extension methods.
    /// </summary>
    public class IntegerSharpFixture
    {
        /// <summary>
        /// Validates that the integer iterating method behaves as expected.
        /// </summary>
        [Test]
        [Row(1, 5, null, 5)]
        [Row(1, 5, 2, 3)]
        [Row(1, 5, 3, 2)]
        public void IntegerStepping(int start, int end, int? step, int expectedNumberOfIterations)
        {
            int numberOfIterations = 0;

            if (step.HasValue)
            {
                foreach (int integer in start.To(end, step.Value))
                {
                    numberOfIterations++;
                }
            }
            else
            {
                foreach (int integer in start.To(end))
                {
                    numberOfIterations++;
                }
            }

            Assert.AreEqual(expectedNumberOfIterations, numberOfIterations);
        }

        /// <summary>
        /// Validates that the integer iterating method behaves as expected.
        /// </summary>
        [Test]
        [Row(1, 5, null, 5)]
        [Row(1, 5, 2, 3)]
        [Row(1, 5, 3, 2)]
        public void IntegerStepping(long start, long end, int? step, long expectedNumberOfIterations)
        {
            long numberOfIterations = 0;

            if (step.HasValue)
            {
                foreach (long integer in start.To(end, step.Value))
                {
                    numberOfIterations++;
                }
            }
            else
            {
                foreach (long integer in start.To(end))
                {
                    numberOfIterations++;
                }
            }

            Assert.AreEqual(expectedNumberOfIterations, numberOfIterations);
        }
    }
}
