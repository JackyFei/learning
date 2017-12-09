using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortingLibrary;

namespace Test.Sorting
{
    [TestClass]
    public class QuickSortXTest : SortTestBase
    {
        protected override ISort CreateSort()
        {
            return new QuickSortX();
        }
    }
}
