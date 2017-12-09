using FundamentalLibrary.Bags;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fundamental.Bags
{
    [TestClass]
    public class LinkedBagTest : BagTestBase
    {
        protected override IBag<int> CreateBag()
        {
            return new LinkedBag<int>();
        }
    }
}
