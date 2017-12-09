using FundamentalLibrary.Stacks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fundamental.Stacks
{
    [TestClass]
    public class ResizingArrayStackTest : StackTestBase
    {
        protected override IStack<int> CreateStack()
        {
            return new ResizingArrayStack<int>(210);
        }
    }
}
