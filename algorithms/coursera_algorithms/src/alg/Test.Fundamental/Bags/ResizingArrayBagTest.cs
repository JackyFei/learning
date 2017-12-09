using FundamentalLibrary.Bags;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fundamental.Bags
{
    [TestClass]
    public class ResizingArrayBagTest : BagTestBase
    {
        protected override IBag<int> CreateBag()
        {
            return new ResizingArrayBag<int>();
        }
    }
}
