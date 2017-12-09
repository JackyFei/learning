using System;

namespace SortingLibrary.PriorityQueues
{
    /// <summary>
    /// Many applications require that we process items having keys in order, but not necessarily in full sorted order and not necessarily all at once.
    /// Often, we collect a set of items, then process the one with the largest key, then perhaps collect more items, then process the one with the current largest key, and so forth. 
    /// An appropriate data type in such an environment supports two operations: remove the maximum and insert.
    /// Such a data type is called a priority queue.
    /// To implement max/min priority queue in one interface, we use Push, Pop, Peek to replace the insert/deleteMaxMin/getMaxMin.
    /// PriorityQueueType indicates if the PQ is MaxPQ or MinPQ.
    /// </summary>
    public interface IPriorityQueue<T> where T : IComparable<T>
    {
        int Size { get; }
        bool IsEmpty { get; }
        PriorityQueueType Type { get; }
        void Enqueue(T obj);
        T Dequeue();
        T Peek();
    }
}
