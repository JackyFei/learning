using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FundamentalLibrary.Queues;

namespace Test.Fundamental.Queues
{
    [TestClass]
    public abstract class QueueTestBase
    {
        protected abstract IQueue<int> CreateQueue();

        [TestMethod]
        public void NewQueue()
        {
            var queue = CreateQueue();

            Assert.IsNotNull(queue);
            Assert.AreEqual(0, queue.Size);
            Assert.IsTrue(queue.IsEmpty);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NewQueue_Dequeue()
        {
            var queue = CreateQueue();

            queue.Dequeue();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NewQueue_Peek()
        {
            var queue = CreateQueue();

            queue.Peek();
        }

        [TestMethod]
        public void Enqueue100AndPeekDequeue100()
        {
            var queue = CreateQueue();

            for (var i = 0; i < 100; i++)
            {
                queue.Enqueue(i + 1);
                Assert.AreEqual(i + 1, queue.Size);
            }

            for (var i = 0; i < 100; i++)
            {
                var value = queue.Peek();
                Assert.AreEqual(i + 1, value);
                Assert.AreEqual(100 - i, queue.Size);

                value = queue.Dequeue();
                Assert.AreEqual(i + 1, value);
                Assert.AreEqual(99 - i, queue.Size);
            }

            Assert.IsTrue(queue.IsEmpty);
        }

        [TestMethod]
        public void Enumerable100()
        {
            var queue = CreateQueue();

            for (var i = 0; i < 100; i++)
            {
                queue.Enqueue(i + 1);
            }

            int value = 1;
            foreach (var v in queue)
            {
                Assert.AreEqual(value, v);
                value++;
            }

            // go through each item again.
            value = 1;
            foreach (var v in queue)
            {
                Assert.AreEqual(value, v);
                value++;
            }
        }

        [TestMethod]
        public void Enumerable0()
        {
            var queue = CreateQueue();

            foreach (var v in queue)
            {
                Assert.Fail();
            }
        }
    }
}
