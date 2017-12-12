using System;
using System.Collections.Generic;

namespace SearchingLibrary
{
    /// <summary>
    /// Interface for symbol table.
    /// The primary purpose of a symbol table is to associate a value with a key. 
    /// The client can insert key–value pairs into the symbol table with the expectation of later being able to search for the value associated with a given key.
    /// </summary>
    public interface ISymbolTable<TKey, TValue> where TKey : IComparable<TKey>
    {
        void Put(TKey key, TValue value);
        TValue Get(TKey key);
        void Delete(TKey key);
        bool Contains(TKey key);
        bool IsEmpty { get; }
        int Size { get; }
        IEnumerable<TKey> Keys { get; }
        TKey Min();
        TKey Max();
        TKey Floor(TKey key);
        TKey Ceiling(TKey key);
    }
}
