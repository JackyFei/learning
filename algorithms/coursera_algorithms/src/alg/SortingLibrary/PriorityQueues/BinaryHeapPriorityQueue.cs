using System;
using System.Diagnostics;

namespace SortingLibrary.PriorityQueues
{
    /// <summary>
    /// This implementation uses a binary heap.
    /// The Enqueue and dequeue operations take logarithmic amortized time.
    /// The Peek, Size, and IsEmpty operations take constant time.
    /// Construction takes time proportional to the specified capacity or the number of items used to initialize the data structure.
    /// Heap definitions. The binary heap is a data structure that can efficiently support the basic priority-queue operations.
    /// In a binary heap, the items are stored in an array such that each key is guaranteed to be larger than (or equal to) the keys at two other specific positions.
    /// In turn, each of those keys must be larger than two more keys, and so forth. This ordering is easy to see if we view the keys as being in a binary tree structure with edges from each key to the two keys known to be smaller.
    /// Definition.A binary tree is heap-ordered if the key in each node is larger than(or equal to) the keys in that nodes two children(if any).
    /// Proposition.The largest key in a heap-ordered binary tree is found at the root.
    /// Definition. A binary heap is a set of nodes with keys arranged in a complete heap-ordered binary tree, represented in level order in an array (not using the first entry).
    /// </summary>
    public class BinaryHeapPriorityQueue<T> : IPriorityQueue<T> where T : IComparable<T>
    {
        private T[] _objArray;

        public BinaryHeapPriorityQueue(int capacity, PriorityQueueType priorityQueueType = PriorityQueueType.MaxPriorityQueue)
        {
            _objArray = new T[capacity + 1];
            Type = priorityQueueType;
            Size = 0;
        }

        #region IPriorityQueue
        public int Size { get; private set; }
        public bool IsEmpty => Size == 0;
        public PriorityQueueType Type { get; }
        public void Enqueue(T obj)
        {
            if(Size == _objArray.Length - 1)
            {
                Resize(_objArray.Length * 2);
            }
            _objArray[++Size] = obj;
            Swim(Size);
        }

        public T Dequeue()
        {
            if (IsEmpty) throw new InvalidOperationException("PriorityQueue has no element.");

            var item = _objArray[1];
            SortHelper.Exch(_objArray, 1, Size--);
            Sink(1);
            // to avoid loiterig and help with garbage collection
            _objArray[Size + 1] = default(T);
            if (Size > 0 && Size == (_objArray.Length - 1) / 4)
            {
                Resize(_objArray.Length / 2);
            }
            return item;
        }

        public T Peek()
        {
            if (IsEmpty) throw new InvalidOperationException("PriorityQueue has no element.");
            return _objArray[1];
        }
        #endregion

        /// <summary>
        /// Bottom-up reheapify (swim).
        /// If the heap order is violated because a node's key becomes larger than that node's parents key, then we can make progress toward fixing the violation by exchanging the node with its parent.
        /// After the exchange, the node is larger than both its children (one is the old parent, and the other is smaller than the old parent because it was a child of that node) but the node may still be larger than its parent.
        /// We can fix that violation in the same way, and so forth, moving up the heap until we reach a node with a larger key, or the root.
        /// </summary>
        private void Swim(int k)
        {
            while (k > 1 && (Type == PriorityQueueType.MaxPriorityQueue && SortHelper.Less(_objArray[k / 2], _objArray[k]) || Type == PriorityQueueType.MinPriorityQueue && SortHelper.Greater(_objArray[k / 2], _objArray[k])))
            {
                SortHelper.Exch(_objArray, k / 2, k);
                k = k / 2;
            }
        }

        /// <summary>
        /// Top-down heapify (sink).
        /// If the heap order is violated because a node's key becomes smaller than one or both of that node's children's keys, then we can make progress toward fixing the violation by exchanging the node with the larger of its two children.
        /// This switch may cause a violation at the child; we fix that violation in the same way, and so forth, moving down the heap until we reach a node with both children smaller, or the bottom.
        /// </summary>
        private void Sink(int k)
        {
            while (2 * k <= Size)
            {
                int j = 2 * k;
                if (j < Size && (Type == PriorityQueueType.MaxPriorityQueue && SortHelper.Less(_objArray[j], _objArray[j + 1]) || Type == PriorityQueueType.MinPriorityQueue && SortHelper.Greater(_objArray[j], _objArray[j + 1]))) j++;
                if (Type == PriorityQueueType.MaxPriorityQueue && SortHelper.Greater(_objArray[k], _objArray[j]) || Type == PriorityQueueType.MinPriorityQueue && SortHelper.Less(_objArray[k], _objArray[j])) break;
                SortHelper.Exch(_objArray, k, j);
                k = j;
            }
        }

        private void Resize(int capacity)
        {
            if (capacity < Size)
            {
                throw new ArgumentException("value should be greater than Size.", nameof(capacity));
            }

            var tempArray = new T[capacity];

            Debug.Assert(_objArray != null, "_objArray != null");
            for (var i = 1; i <= Size; i++)
            {
                tempArray[i] = _objArray[i];
            }

            _objArray = tempArray;
        }
    }
}
