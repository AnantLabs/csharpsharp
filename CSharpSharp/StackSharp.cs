using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CSharpSharp
{
    /// <summary>
    /// Provides a stack with a slightly different behavior than the standard <see cref="Stack&lt;T&gt;"/>.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    [DebuggerDisplay("{Item}, {ActualStack.Count, nq} levels")]
    public class StackSharp<TItem>
    {
        /// <summary>
        /// Gets or sets the actual stack.
        /// </summary>
        /// <value>
        /// The actual stack.
        /// </value>
        private Stack<StackSharp<TItem>> ActualStack { get; set; }

        /// <summary>
        /// Gets the parent node of the current node.
        /// </summary>
        /// <value>The parent.</value>
        public StackSharp<TItem> ParentNode { get; private set; }

        /// <summary>
        /// Gets the item in the current node.
        /// </summary>
        /// <value>The item.</value>
        public TItem Item { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is root.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is root; otherwise, <c>false</c>.
        /// </value>
        private bool HasItem { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StackSharp&lt;TItem&gt;"/> class.
        /// </summary>
        public StackSharp()
        {
            this.ActualStack = new Stack<StackSharp<TItem>>();
            this.HasItem = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StackSharp&lt;TItem&gt;"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        private StackSharp(TItem item)
        {
            this.Item = item;
            this.HasItem = true;
        }

        /// <summary>
        /// Gets the element at the bottom of the stack.
        /// </summary>
        /// <value>The top.</value>
        public StackSharp<TItem> Root
        {
            get
            {
                StackSharp<TItem> potentialRoot = this;
                while (potentialRoot.ParentNode != null)
                {
                    potentialRoot = potentialRoot.ParentNode;
                }

                return potentialRoot;
            }
        }

        /// <summary>
        /// Pushes an item on the stack and sets the current node as the new node.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Push(TItem item)
        {
            StackSharp<TItem> newParent = null;

            // Set the parent of the new element to the current element (if the current element exists)
            if (this.HasItem)
            {
                newParent = new StackSharp<TItem>(this.Item);
                newParent.ParentNode = this.ParentNode;
                this.ActualStack.Push(newParent);
            }
            else
            {
                this.HasItem = true;
            }

            // Replace the content of the current element (which is always the last level) with the new content
            this.Item = item;

            // Set the parent of the current element with the new parent
            this.ParentNode = newParent;
        }

        /// <summary>
        /// Removes the element at the top of the stack and returns its content.
        /// </summary>
        /// <returns>The content of the popped element.</returns>
        public TItem Pop()
        {
            TItem currentItem = this.Item;

            // Pop the parent of the current element (it will become the new current element)
            StackSharp<TItem> secondToLastElement = this.ActualStack.Pop();

            this.Item = secondToLastElement.Item;
            this.ParentNode = secondToLastElement.ParentNode;

            return currentItem;
        }
    }
}
