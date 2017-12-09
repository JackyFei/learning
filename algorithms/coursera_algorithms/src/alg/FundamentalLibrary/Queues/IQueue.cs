using System.Collections.Generic;

namespace FundamentalLibrary.Queues
{
    /// <summary>
    /// First-in-first-out (FIFO) queue of generic items. 
    /// It supports the usual <see cref="Enqueue"/> and <see cref="Dequeue"/> operations, along with methods for peeking at the first item, testing if the queue is empty, and iterating through the items in FIFO order.
    /// </summary>
    public interface IQueue<T> : IEnumerable<T>
    {
        bool IsEmpty { get; }
        int Size { get; }
        void Enqueue(T item);
        T Dequeue();
        T Peek();
    }
}
