using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortingLibrary;

namespace Test.Sorting
{
    [TestClass]
    public class HeapSortTest : SortTestBase
    {
        protected override ISort CreateSort()
        {
            return new HeapSort();
        }
    }
}
