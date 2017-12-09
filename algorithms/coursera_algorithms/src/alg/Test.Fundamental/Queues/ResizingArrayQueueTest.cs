using System;
using FundamentalLibrary.Queues;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fundamental.Queues
{
    [TestClass]
    public class ResizingArrayQueueTest : QueueTestBase
    {
        protected override IQueue<int> CreateQueue()
        {
            return new ResizingArrayQueue<int>();
        }
    }
}
