using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortingLibrary.PriorityQueues;

namespace Test.Sorting.PriorityQueues
{
    [TestClass]
    public class BinaryHeapPriorityQueueTest : PriorityQueueTestBase
    {
        protected override IPriorityQueue<T> CreatePriorityQueue<T>(int capacity,
            PriorityQueueType priorityQueueType = PriorityQueueType.MaxPriorityQueue)
        {
            return new BinaryHeapPriorityQueue<T>(capacity, priorityQueueType);
        }
    }
}
