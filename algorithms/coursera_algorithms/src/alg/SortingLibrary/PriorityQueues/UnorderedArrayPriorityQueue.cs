using System;
using System.Diagnostics;

namespace SortingLibrary.PriorityQueues
{
    /// <summary>
    /// Priority queue implementation with an unsorted array.
    /// </summary>
    public class UnorderedArrayPriorityQueue<T> : IPriorityQueue<T> where T : IComparable<T>
    {
        private T[] _objArray;

        public UnorderedArrayPriorityQueue(int capacity, PriorityQueueType priorityQueueType = PriorityQueueType.MaxPriorityQueue)
        {
            Type = priorityQueueType;
            _objArray = new T[capacity];
            Size = 0;
        }

        #region IPriorityQueue
        public int Size { get; private set; }

        public bool IsEmpty => Size == 0;

        public PriorityQueueType Type { get; private set; }

        public T Peek()
        {
            if (IsEmpty) throw new InvalidOperationException("PriorityQueue has no element.");
            int itemIndex = GetIndex();
            return _objArray[itemIndex];
        }

        public T Dequeue()
        {
            if (IsEmpty) throw new InvalidOperationException("PriorityQueue has no element.");
            int itemIndex = GetIndex();
            SortHelper.Exch(_objArray, Size - 1, itemIndex);
            var result = _objArray[--Size];
            if(Size == _objArray.Length / 4)
            {
                Resize(_objArray.Length / 2);
            }
            return result;
        }

        public void Enqueue(T obj)
        {
            if (Size == _objArray.Length)
                Resize(_objArray.Length * 2);
            _objArray[Size++] = obj;
        }
        #endregion

        private int GetIndex()
        {
            int itemIndex = 0;
            for (var i = 0; i < Size; i++)
            {
                if (Type == PriorityQueueType.MinPriorityQueue && SortHelper.Less(_objArray[i], _objArray[itemIndex])
                    || Type == PriorityQueueType.MaxPriorityQueue && SortHelper.Greater(_objArray[i], _objArray[itemIndex])) itemIndex = i;
            }
            return itemIndex;
        }

        private void Resize(int capacity)
        {
            if (capacity < Size)
            {
                throw new ArgumentException("value should be greater than Size.", nameof(capacity));
            }

            var tempArray = new T[capacity];

            Debug.Assert(_objArray != null, "_objArray != null");
            for (var i = 0; i < Size; i++)
            {
                tempArray[i] = _objArray[i];
            }

            _objArray = tempArray;
        }
    }
}
