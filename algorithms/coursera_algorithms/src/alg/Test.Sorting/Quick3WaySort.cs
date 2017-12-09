using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortingLibrary;

namespace Test.Sorting
{
    [TestClass]
    public class Quick3WaySort : SortTestBase
    {
        protected override ISort CreateSort()
        {
            return new SortingLibrary.Quick3WaySort();
        }
    }
}
