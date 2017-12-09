using System.Collections.Generic;

namespace FundamentalLibrary.Stacks
{
    /// <summary>
    /// The <see cref="IStack{T}"/> interface represents a last-in-first-out (LIFO) stack of generic items.
    /// It supports the usual <see cref="Push(T)"/> and <see cref="Pop"/> operations, along with methods for peeking at the top item, testing if the stack is empty, and iterating through the items in LIFO order.
    /// </summary>
    public interface IStack<T> : IEnumerable<T>
    {
        bool IsEmpty { get; }

        int Size { get; }

        void Push(T item);

        T Pop();

        T Peek();
    }
}
