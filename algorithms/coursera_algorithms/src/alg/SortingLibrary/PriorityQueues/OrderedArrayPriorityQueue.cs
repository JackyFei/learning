using System;
using System.Diagnostics;

namespace SortingLibrary.PriorityQueues
{
    /// <summary>
    /// Priority queue implementation with an ordered array.
    /// </summary>
    public class OrderedArrayPriorityQueue<T> : IPriorityQueue<T> where T : IComparable<T>
    {
        private T[] _objArray;
        public OrderedArrayPriorityQueue(int capacity, PriorityQueueType priorityQueueType = PriorityQueueType.MaxPriorityQueue)
        {
            Type = priorityQueueType;
            _objArray = new T[capacity];
            Size = 0;
        }

        #region IPriorityQueue
        public int Size { get; private set; }

        public bool IsEmpty => Size == 0;

        public PriorityQueueType Type { get; private set; }

        public T Dequeue()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Priority queue has no element.");
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
            var i = Size - 1;
            while (i >= 0 && (Type == PriorityQueueType.MinPriorityQueue && SortHelper.Less(_objArray[i], obj) || Type == PriorityQueueType.MaxPriorityQueue && SortHelper.Greater(_objArray[i], obj)))
            {
                _objArray[i + 1] = _objArray[i];
                i--;
            }
            _objArray[i + 1] = obj;
            Size++;
        }

        public T Peek()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Priority queue has no element.");
            return _objArray[Size - 1];
        }
        #endregion

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
