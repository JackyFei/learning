using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortingLibrary;

namespace Test.Sorting
{
    [TestClass]
    public class MergeSortTest : SortTestBase
    {
        protected override ISort CreateSort()
        {
            return new MergeSort();
        }
    }
}
