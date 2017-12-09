using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortingLibrary;

namespace Test.Sorting
{
    [TestClass]
    public class MergeXSortTest : SortTestBase
    {
        protected override ISort CreateSort()
        {
            return new MergeXSort();
        }
    }
}
