using FundamentalLibrary.Bags;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fundamental.Bags
{
    [TestClass]
    public abstract class BagTestBase
    {
        protected abstract IBag<int> CreateBag();

        [TestMethod]
        public void NewBag()
        {
            var bag = CreateBag();

            Assert.IsNotNull(bag);
            Assert.IsTrue(bag.IsEmpty);
            Assert.AreEqual(0, bag.Size);
        }

        [TestMethod]
        public void Add100()
        {
            var bag = CreateBag();

            for (var i = 1; i <= 100; i++)
            {
                bag.Add(i);
                Assert.AreEqual(i, bag.Size);
            }

            var expectedValue = 100;
            foreach (var i in bag)
            {
                Assert.AreEqual(expectedValue, i);
                expectedValue--;
            }
        }
    }
}
