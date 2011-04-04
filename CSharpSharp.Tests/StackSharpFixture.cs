using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;

namespace CSharpSharp.Tests
{
    /// <summary>
    /// Validates the StackSharp object behaves as expected.
    /// </summary>
    public class StackSharpFixture
    {
        /// <summary>
        /// Validates that pushing an item on an empty stack sets the item as the root.
        /// </summary>
        [Test]
        public void PushOnEmptyStack()
        {
            StackSharp<int> stack = new StackSharp<int>();

            stack.Push(1);

            Assert.AreEqual(1, stack.Item);
            Assert.AreEqual(1, stack.Root.Item);
            Assert.IsNull(stack.ParentNode);
        }

        /// <summary>
        /// Validates that popping an empty stack raises an exception.
        /// </summary>
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PopEmptyStack()
        {
            StackSharp<int> stack = new StackSharp<int>();

            stack.Pop();
        }

        /// <summary>
        /// Validates that some standard operations behave as expected.
        /// </summary>
        [Test]
        public void StandardOperationsTest()
        {
            StackSharp<string> stack = new StackSharp<string>();
            stack.Push("root");
            stack.Push("first level");
            stack.Push("second level");

            Assert.AreEqual("second level", stack.Item);
            Assert.AreEqual("first level", stack.ParentNode.Item);
            Assert.AreEqual("root", stack.ParentNode.ParentNode.Item);
            Assert.AreEqual("root", stack.Root.Item);

            string poppedString = stack.Pop();

            Assert.AreEqual(poppedString, "second level");
            Assert.AreEqual(stack.Item, "first level");

            Stack<int> asdf = new Stack<int>();
            asdf.Push(1);
            var value = asdf.Pop();

            Assert.AreEqual(1, value);
        }
    }
}
