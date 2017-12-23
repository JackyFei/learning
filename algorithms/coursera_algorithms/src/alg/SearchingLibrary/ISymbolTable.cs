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
        bool TryGet(TKey key, out TValue value);
        void Delete(TKey key);
        bool Contains(TKey key);
        bool IsEmpty { get; }
        int Size { get; }
        IEnumerable<TKey> Keys { get; }
        bool TryGetMin(out TKey key);
        bool TryGetMax(out TKey key);
        bool TryGetFloor(TKey key, out TKey keyResult);
        bool TryGetCeiling(TKey key, out TKey keyResult);
        void DeleteMin();
        void DeleteMax();
    }
}
