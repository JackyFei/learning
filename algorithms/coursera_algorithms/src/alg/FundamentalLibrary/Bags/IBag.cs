using System.Collections.Generic;

namespace FundamentalLibrary.Bags
{
    /// <summary>
    /// A bag is a collection where removing items is not supported—its purpose is to provide clients with the ability to collect items and then to iterate through the collected items.
    /// </summary>
    public interface IBag<T> : IEnumerable<T>
    {
        int Size { get; }
        bool IsEmpty { get; }
        void Add(T item);
    }
}
