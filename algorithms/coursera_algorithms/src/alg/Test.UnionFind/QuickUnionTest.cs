using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnionFindLibrary;

namespace Test.UnionFind
{
    [TestClass]
    public class QuickUnionTest : UnionFindTestBase
    {
        protected override IUnionFind CreateUnionFind(int n)
        {
            return new QuickUnion(n);
        }
    }
}
