using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortingLibrary;

namespace Test.Sorting
{
    [TestClass]
    public class ShellSortTest : SortTestBase
    {
        protected override ISort CreateSort()
        {
            return new ShellSort();
        }
    }
}
