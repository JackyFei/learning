using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchingLibrary;

namespace Test.Searching
{
    [TestClass]
    public class UnorderedSequentialSearchSymbolTableTest : SymbolTableTestBase
    {
        protected override ISymbolTable<TKey, TValue> CreateSymbolTable<TKey, TValue>()
        {
            return new UnorderedSequentialSearchSymbolTable<TKey, TValue>();
        }
    }
}
