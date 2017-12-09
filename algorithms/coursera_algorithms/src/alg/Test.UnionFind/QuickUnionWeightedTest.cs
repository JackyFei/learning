using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnionFindLibrary;

namespace Test.UnionFind
{
    [TestClass]
    public class QuickUnionWeightedTest : UnionFindTestBase
    {
        protected override IUnionFind CreateUnionFind(int n)
        {
            return new QuickUnionWeighted(n);
        }
    }
}
