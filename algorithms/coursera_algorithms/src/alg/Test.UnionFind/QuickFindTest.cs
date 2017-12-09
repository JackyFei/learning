using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnionFindLibrary;

namespace Test.UnionFind
{
    [TestClass]
    public class QuickFindTest : UnionFindTestBase
    {
        protected override IUnionFind CreateUnionFind(int n)
        {
            return new QuickFind(n);
        }
    }
}
