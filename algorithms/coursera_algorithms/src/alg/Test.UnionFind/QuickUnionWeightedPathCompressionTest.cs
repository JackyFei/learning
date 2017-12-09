using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnionFindLibrary;

namespace Test.UnionFind
{
    [TestClass]
    public class QuickUnionWeightedPathCompressionTest : UnionFindTestBase
    {
        protected override IUnionFind CreateUnionFind(int n)
        {
            return new QuickUnionWeightedPathCompression(n);
        }
    }
}
