using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortingLibrary;
using System.Collections.Generic;

namespace Test.Sorting
{
    [TestClass]
    public abstract class SortTestBase
    {
        private static readonly Random Random = new Random();
        private static readonly Dictionary<int, int[]> RandomArrayDictionary = new Dictionary<int, int[]>();
        private static readonly object LockObj = new object();

        protected abstract ISort CreateSort();

        [TestMethod]
        public void SimplySort()
        {
            var sort = CreateSort();
            var array = new[] {9, 8, 7, 6, 5, 4, 3, 2, 1};
            sort.Sort(array);
            Assert.IsTrue(SortHelper.IsSorted(array));

            sort.Sort(array, SortOrder.DESC);
            Assert.IsTrue(SortHelper.IsSorted(array, SortOrder.DESC));
        }

        [TestMethod]
        public void Array100()
        {
            var sort = CreateSort();
            var array = RandomArray(100);
            sort.Sort(array);
            Assert.IsTrue(SortHelper.IsSorted(array));
        }

        [TestMethod]
        public void Array10000()
        {
            var sort = CreateSort();
            var array = RandomArray(10000);
            sort.Sort(array);
            Assert.IsTrue(SortHelper.IsSorted(array));
        }

        [TestMethod]
        public void Array20000()
        {
            var sort = CreateSort();
            var array = RandomArray(20000);
            sort.Sort(array);
            Assert.IsTrue(SortHelper.IsSorted(array));
        }

        [TestMethod]
        public void Array100000()
        {
            var sort = CreateSort();
            var array = RandomArray(100000);
            sort.Sort(array);
            Assert.IsTrue(SortHelper.IsSorted(array));
        }

        private static int[] RandomArray(int n)
        {
            lock (LockObj)
            {
                if (RandomArrayDictionary.ContainsKey(n)) return RandomArrayDictionary[n].Clone() as int[];
                var array = new int[n];
                // max is smaller than n, then it must have duplicate items.
                var max = n / 2;
                for (var i = 0; i < n; i++)
                {
                    array[i] = Random.Next(max);
                }
                RandomArrayDictionary.Add(n, array);
                return array.Clone() as int[];
            }
        }
    }
}
