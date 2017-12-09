using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnionFindLibrary;

namespace Test.UnionFind
{
    [TestClass]
    public abstract class UnionFindTestBase
    {
        protected abstract IUnionFind CreateUnionFind(int n);
        private static List<Tuple<int, int>> _pqs;
        private static readonly object _lockObj = new object();

        [TestMethod]
        public void NewUnionFind()
        {
            var unionFind = CreateUnionFind(10);

            Assert.AreEqual(10, unionFind.Count);
        }

        [TestMethod]
        public void UnionFind_Find()
        {
            var unionFind = CreateUnionFind(10);
            unionFind.Union(1, 5);
            unionFind.Union(5, 7);
            Assert.AreEqual(unionFind.Find(1), unionFind.Find(5));
            Assert.AreEqual(unionFind.Find(1), unionFind.Find(7));
            Assert.AreEqual(unionFind.Find(5), unionFind.Find(7));
            Assert.AreEqual(8, unionFind.Count);
        }

        [TestMethod]
        public void UnionFind_3Unions()
        {
            var unionFind = CreateUnionFind(5);
            unionFind.Union(0, 1);
            unionFind.Union(1, 2);
            unionFind.Union(2, 4);

            Assert.IsTrue(unionFind.Connected(0, 1));
            Assert.IsTrue(unionFind.Connected(0, 2));
            Assert.IsTrue(unionFind.Connected(0, 4));
            Assert.IsTrue(unionFind.Connected(1, 2));
            Assert.IsTrue(unionFind.Connected(1, 4));
            Assert.IsTrue(unionFind.Connected(2, 4));

            Assert.IsFalse(unionFind.Connected(0, 3));
            Assert.IsFalse(unionFind.Connected(1, 3));
            Assert.IsFalse(unionFind.Connected(2, 3));
            Assert.IsFalse(unionFind.Connected(3, 4));
            Assert.AreEqual(2, unionFind.Count);
        }

        [TestMethod]
        public void UnionFind_50000Union()
        {
            const int n = 50000;
            var unionFind = CreateUnionFind(n);
            var pqs = GeneratePQs(n);

            foreach(var pq in pqs)
            {
                unionFind.Union(pq.Item1, pq.Item2);
            }
        }

        private static Tuple<int, int> GetRandomPAndQ(Random random, ISet<int> set, int max)
        {
            var p = 0;
            var q = 0;
            while (p == q || set.Contains(p * 100000 + q))
            {
                p = random.Next(max);
                q = random.Next(max);
                if (p <= q) continue;
                var tmp = p;
                p = q;
                q = tmp;
            }
            set.Add(p * 100000 + q);
            return new Tuple<int, int>(p, q);
        }

        private static List<Tuple<int,int>> GeneratePQs(int n)
        {
            lock (_lockObj)
            {
                if(_pqs == null)
                {
                    _pqs = new List<Tuple<int, int>>();

                    var random = new Random();
                    var set = new HashSet<int>();

                    for (var i = 0; i < n; i++)
                    {
                        var pq = GetRandomPAndQ(random, set, n - 1);
                        _pqs.Add(pq);
                    }
                }
                return _pqs;
            }
        }
    }
}
