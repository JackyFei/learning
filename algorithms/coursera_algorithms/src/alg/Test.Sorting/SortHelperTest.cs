using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortingLibrary;

namespace Test.Sorting
{
    [TestClass]
    public class SortHelperTest
    {
        [TestMethod]
        public void Less()
        {
            Assert.IsTrue(SortHelper.Less(5, 5.1));
            Assert.IsFalse(SortHelper.Less(5.1, 5));
            Assert.IsFalse(SortHelper.Less(5, 5));
        }

        [TestMethod]
        public void Greater()
        {
            Assert.IsFalse(SortHelper.Greater(5, 5.1));
            Assert.IsFalse(SortHelper.Greater(5, 5));
            Assert.IsTrue(SortHelper.Greater(5.1, 5));
        }

        [TestMethod]
        public void Exch()
        {
            var array = new [] {0, 1, 2, 3, 4, 5};

            SortHelper.Exch(array, 0, 5);
            SortHelper.Exch(array, 1, 4);
            SortHelper.Exch(array, 2, 3);

            Assert.AreEqual(5, array[0]);
            Assert.AreEqual(0, array[5]);
            Assert.AreEqual(1, array[4]);
            Assert.AreEqual(4, array[1]);
            Assert.AreEqual(3, array[2]);
            Assert.AreEqual(2, array[3]);
        }

        [TestMethod]
        public void IsSorted()
        {
            Assert.IsTrue(SortHelper.IsSorted(new[] {1, 2, 3, 4, 4, 6, 7, 9, 12}));
            Assert.IsTrue(SortHelper.IsSorted(new[] {123, 21, 14, 10, 10, 3, 1}, SortOrder.DESC));
            Assert.IsFalse(SortHelper.IsSorted(new[] {1, 3, 1, 5, 2, 7, 9, 13}));
            Assert.IsFalse(SortHelper.IsSorted(new[] {1, 3, 1, 5, 2, 7, 9, 13}, SortOrder.DESC));
        }

        [TestMethod]
        public void Shuffle()
        {
            var a = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            SortHelper.Shuffle(a);
            Assert.AreEqual(10, a.Length);
            Assert.IsFalse(SortHelper.IsSorted(a));
        }

        [TestMethod]
        public void Compare()
        {
            Assert.AreEqual(1, SortHelper.Compare(5, 1));
            Assert.AreEqual(0, SortHelper.Compare(1, 1));
            Assert.AreEqual(-1, SortHelper.Compare(1, 5));
        }
    }
}
