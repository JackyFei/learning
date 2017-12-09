using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortingLibrary;

namespace Test.Sorting
{
    [TestClass]
    public class QuickSortTest : SortTestBase
    {
        protected override ISort CreateSort()
        {
            return new QuickSort();
        }
    }
}
