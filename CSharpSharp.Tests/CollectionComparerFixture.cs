using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;

namespace CSharpSharp.Tests
{
    /// <summary>
    /// Validates the methods of the CollectionComparer class.
    /// </summary>
    public class CollectionComparerFixture
    {
        /// <summary>
        /// Validates that collections with the same objects are the same.
        /// </summary>
        [Test]
        public void CompareEqualCollectionsTest()
        {
            List<int> first = new List<int>();
            first.Add(1);
            first.Add(2);
            first.Add(3);

            List<int> second = new List<int>();
            second.Add(3);
            second.Add(2);
            second.Add(1);

            Assert.AreNotEqual(first, second);
            Assert.IsTrue(new CollectionComparer<int>().AreEqual(first, second));
        }

        /// <summary>
        /// Validates that collections without the same objects are the same.
        /// </summary>
        [Test]
        public void CompareNotEqualCollectionsTest()
        {
            List<int> first = new List<int>();
            first.Add(1);
            first.Add(1);
            first.Add(2);
            first.Add(3);

            List<int> second = new List<int>();
            second.Add(3);
            second.Add(2);
            second.Add(1);

            Assert.AreNotEqual(first, second);
            Assert.IsFalse(new CollectionComparer<int>().AreEqual(first, second));
        }

        /// <summary>
        /// Validates that collections of reference objects with the same objects are the same
        /// only when using a sameness comparer.
        /// </summary>
        [Test]
        public void CompareCollectionsOfReferenceObjectsTest()
        {
            List<DummyClass> first = new List<DummyClass>();
            first.Add(new DummyClass(1));
            first.Add(new DummyClass(2));
            first.Add(new DummyClass(3));

            List<DummyClass> second = new List<DummyClass>();
            second.Add(new DummyClass(3));
            second.Add(new DummyClass(2));
            second.Add(new DummyClass(1));

            Assert.AreNotEqual(first, second);
            Assert.IsFalse(new CollectionComparer<DummyClass>().AreEqual(first, second));
            Assert.IsTrue(new CollectionComparer<DummyClass>(
                delegate(DummyClass foo, DummyClass bar)
                {
                    return foo.Value == bar.Value;
                }).AreEqual(first, second));
        }

        /// <summary>
        /// Validates that comparing collections that have a null
        /// item does not fail.
        /// </summary>
        [Test]
        public void CompareCollectionsWithNullItems()
        {
            List<string> first = new List<string>();
            first.Add("one");
            first.Add(null);

            List<string> second = new List<string>();
            second.Add("one");
            second.Add("two");

            Assert.IsFalse(new CollectionComparer<string>().AreEqual(first, second));
        }

        /// <summary>
        /// Represents a dummy class used in tests.
        /// </summary>
        private class DummyClass
        {
            /// <summary>
            /// Gets or sets the value.
            /// </summary>
            /// <value>
            /// The value.
            /// </value>
            public int Value { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="DummyClass"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public DummyClass(int value)
            {
                this.Value = value;
            }
        }
    }
}
