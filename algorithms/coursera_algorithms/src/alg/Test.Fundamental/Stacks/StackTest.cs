using Microsoft.VisualStudio.TestTools.UnitTesting;
using FundamentalLibrary.Stacks;
using System;

namespace Test.Fundamental.Stacks
{
    [TestClass]
    public abstract class StackTestBase
    {
        protected abstract IStack<int> CreateStack();

        [TestMethod]
        public void NewStack()
        {
            var stack = CreateStack();

            Assert.IsNotNull(stack);
            Assert.AreEqual(0, stack.Size);
            Assert.IsTrue(stack.IsEmpty);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NewStack_Pop()
        {
            var stack = CreateStack();

            stack.Pop();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NewStack_Peek()
        {
            var stack = CreateStack();

            stack.Peek();
        }

        [TestMethod]
        public void Push100AndPeekPop100()
        {
            var stack = CreateStack();

            for (var i = 0; i < 100; i++)
            {
                stack.Push(i + 1);
                Assert.AreEqual(i + 1, stack.Size);
            }

            for (var i = 0; i < 100; i++)
            {
                var value = stack.Peek();
                Assert.AreEqual(100 - i, value);
                Assert.AreEqual(100 - i, stack.Size);

                value = stack.Pop();
                Assert.AreEqual(100 - i, value);
                Assert.AreEqual(99 - i, stack.Size);
            }

            Assert.IsTrue(stack.IsEmpty);
        }

        [TestMethod]
        public void Enumerable100()
        {
            var stack = CreateStack();

            for (var i = 0; i < 100; i++)
            {
                stack.Push(i + 1);
            }

            int value = 100;
            foreach (var v in stack)
            {
                Assert.AreEqual(value, v);
                value--;
            }

            // go through each item again.
            value = 100;
            foreach (var v in stack)
            {
                Assert.AreEqual(value, v);
                value--;
            }
        }

        [TestMethod]
        public void Enumerable0()
        {
            var stack = CreateStack();

            foreach (var v in stack)
            {
                Assert.Fail();
            }
        }
    }
}
