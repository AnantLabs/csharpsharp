using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpSharp
{
    /// <summary>
    /// Represents a service used to compare two collections for equality.
    /// </summary>
    /// <typeparam name="T">The type of the items in the collections.</typeparam>
    public class CollectionComparer<T>
    {
        #region Delegate

        /// <summary>
        /// Compares two object instances represent the same object.
        /// </summary>
        /// <param name="foo">The first object.</param>
        /// <param name="bar">The second object.</param>
        /// <returns>True if both instances represent the same object, false otherwise.</returns>
        public delegate bool SamenessComparer(T foo, T bar);

        #endregion // Delegate


        #region Fields

        /// <summary>
        /// The delegate used to compare two T objects for equality.
        /// </summary>
        private SamenessComparer samenessComparer;

        #endregion // Fields


        #region Constructors

        /// <summary>
        /// Creates a new instance of a collection comparer that compares two collections for equality.
        /// </summary>
        public CollectionComparer()
        {
            // Set a default sameness comparer
            this.samenessComparer =
                delegate(T firstInstance, T secondInstance)
                {
                    return firstInstance.Equals(secondInstance);
                };
        }

        /// <summary>
        /// Creates a new instance of a collection comparer with the specified comparer.
        /// </summary>
        /// <param name="samenessComparer">Delegate that checks if two instances represent the same object.</param>
        public CollectionComparer(SamenessComparer samenessComparer)
        {
            this.samenessComparer = samenessComparer;
        }

        #endregion // Constructors


        /// <summary>
        /// Compares the content of two collections for equality.
        /// </summary>
        /// <param name="foo">The first collection.</param>
        /// <param name="bar">The second collection.</param>
        /// <returns>True if both collections have the same content, false otherwise.</returns>
        public bool AreEqual(ICollection<T> foo, ICollection<T> bar)
        {
            // Declare a dictionary to count the occurence of the items in the collection
            Dictionary<T, int> itemCounts = new Dictionary<T, int>();
            int nullItemsCount = 0;

            // Increase the count for each occurence of the item in the first collection
            foreach (T item in foo)
            {
                // Increase the null counter when the item is null (it can't be used as a key)
                if (ReferenceEquals(item, null))
                {
                    nullItemsCount++;
                    continue;
                }

                if (itemCounts.ContainsKey(item))
                {
                    itemCounts[item]++;
                }
                else
                {
                    itemCounts[item] = 1;
                }
            }

            // Wrap the keys in a searchable list
            List<T> keys = new List<T>(itemCounts.Keys);

            // Decrease the count for each occurence of the item in the second collection
            foreach (T item in bar)
            {
                // Decrease the null counter when the item is null (it can't be used as a key)
                if (ReferenceEquals(item, null))
                {
                    nullItemsCount--;
                    continue;
                }

                // Try to find a key for the item
                T key = keys.Find(
                    delegate(T listKey)
                    {
                        return this.samenessComparer(listKey, item);
                    });

                // Check if a key was found
                if (key != null)
                {
                    itemCounts[key]--;
                }
                else
                {
                    // There was no occurence of this item in the first collection, thus the collections are not equal
                    return false;
                }
            }

            // The count of null items should be 0 if the contents of the collections are equal
            if (nullItemsCount != 0)
            {
                return false;
            }

            // The count of each item should be 0 if the contents of the collections are equal
            foreach (int value in itemCounts.Values)
            {
                if (value != 0)
                {
                    return false;
                }
            }

            // The collections are equal
            return true;
        }
    }
}
