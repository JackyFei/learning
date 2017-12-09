using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortingLibrary.PriorityQueues;

namespace Test.Sorting.PriorityQueues
{
    [TestClass]
    public abstract class PriorityQueueTestBase
    {
        protected abstract IPriorityQueue<T> CreatePriorityQueue<T>(int capacity, PriorityQueueType priorityQueueType = PriorityQueueType.MaxPriorityQueue) where T : IComparable<T>;

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NoElementDequeue_InvalidOperation()
        {
            var pq = CreatePriorityQueue<int>(10);
            pq.Dequeue();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NoElementPeek_InvalidOperation()
        {
            var pq = CreatePriorityQueue<int>(10);
            pq.Peek();
        }

        [TestMethod]
        public void SimplyMinPQ()
        {
            var pq = CreatePriorityQueue<int>(10, PriorityQueueType.MinPriorityQueue);
            pq.Enqueue(10);
            Assert.AreEqual(10, pq.Peek());
            pq.Enqueue(3);
            Assert.AreEqual(3, pq.Peek());
            Assert.AreEqual(3, pq.Dequeue());
            Assert.AreEqual(10, pq.Dequeue());
        }

        [TestMethod]
        public void Items10()
        {
            ItemN(10);
        }

        [TestMethod]
        public void Items1000()
        {
            ItemN(1000);
        }

        [TestMethod]
        public void Items50000()
        {
            ItemN(50000);
        }

        private void ItemN(int n)
        {
            var pq = CreatePriorityQueue<int>(n/4);

            for (var i = 1; i <= n; i++)
            {
                Assert.AreEqual(i - 1, pq.Size);
                pq.Enqueue(i);
                var max = pq.Peek();
                Assert.AreEqual(i, max);
            }

            for (var i = n; i >= 1; i--)
            {
                Assert.AreEqual(i, pq.Size);
                var max = pq.Dequeue();
                Assert.AreEqual(i, max);
            }

            Assert.AreEqual(0, pq.Size);
            Assert.IsTrue(pq.IsEmpty);
        }
    }
}
