using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortingLibrary;

namespace Test.Sorting
{
    [TestClass]
    public class SelectionSortTest : SortTestBase
    {
        protected override ISort CreateSort()
        {
            return new SelectionSort();
        }
    }
}
