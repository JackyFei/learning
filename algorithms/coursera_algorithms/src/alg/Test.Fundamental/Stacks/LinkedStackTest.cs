using Microsoft.VisualStudio.TestTools.UnitTesting;
using FundamentalLibrary.Stacks;

namespace Test.Fundamental.Stacks
{
    [TestClass]
    public class LinkedStackTest : StackTestBase
    {
        protected override IStack<int> CreateStack()
        {
            return new LinkedStack<int>();
        }
    }
}
