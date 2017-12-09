using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnionFindLibrary;

namespace Test.UnionFind
{
    [TestClass]
    public class QuickUnionPathCompressionTest : UnionFindTestBase
    {
        protected override IUnionFind CreateUnionFind(int n)
        {
            return new QuickUnionPathCompression(n);
        }
    }
}
