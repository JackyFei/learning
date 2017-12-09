using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortingLibrary;

namespace Test.Sorting
{
    [TestClass]
    public class InsertionSortTest : SortTestBase
    {
        protected override ISort CreateSort()
        {
            return new InsertionSort();
        }
    }
}
