using FundamentalLibrary.Queues;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fundamental.Queues
{
    [TestClass]
    public class LinkedQueueTest : QueueTestBase
    {
        protected override IQueue<int> CreateQueue()
        {
            return new LinkedQueue<int>();
        }
    }
}
