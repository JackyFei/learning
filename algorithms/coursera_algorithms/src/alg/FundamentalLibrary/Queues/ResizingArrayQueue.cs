using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace FundamentalLibrary.Queues
{
    /// <summary>
    /// This implementation uses a resizing array, which double the underlying array when it is full and halves the underlying array when it is one-quarter full.
    /// The <see cref="Enqueue"/> and <see cref="Dequeue"/> operations take constant amortized time.
    /// The <see cref="Size"/>, <see cref="Peek"/>, and <see cref="IsEmpty"/> operations takes constant time in the worst case. 
    /// </summary>
    public class ResizingArrayQueue<T> : IQueue<T>
    {
        private T[] _array;
        private int _first;
        private int _last;

        public ResizingArrayQueue(int initialCapacity = 10)
        {
            if (initialCapacity < 1)
                throw new ArgumentException("value should be greater than zero.", nameof(initialCapacity));

            _array = new T[2 * initialCapacity];
            Size = 0;
            _first = 0;
            _last = 0;
        }

        #region IEnumerable<T>
        public IEnumerator<T> GetEnumerator()
        {
            return new ArrayEnumerator(_array, _first, Size);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class ArrayEnumerator : IEnumerator<T>
        {
            private readonly T[] _array;
            private readonly int _size;
            private readonly int _first;
            private int _n;

            public ArrayEnumerator(T[] array, int first, int size)
            {
                _array = array;
                _first = first;
                _size = size;
                _n = 0;
            }

            public void Dispose()
            {
                
            }

            public bool MoveNext()
            {
                if (_n >= _size) return false;
                _n++;
                return true;
            }

            public void Reset()
            {
                throw new NotSupportedException();
            }

            public T Current
            {
                get
                {
                    Debug.Assert(_array != null, "_array != null");
                    var item = _array[(_first + _n - 1) % _array.Length];
                    return item;
                }
            }

            object IEnumerator.Current => Current;
        }
        #endregion

        #region IQueue<T>

        public bool IsEmpty => Size == 0;
        public int Size { get; private set; }
        public void Enqueue(T item)
        {
            Debug.Assert(_array != null, "_array != null");
            if (_array.Length == Size)
            {
                Resize(2 * _array.Length);
            }
            Debug.Assert(_array != null, "_array != null");
            _array[_last++] = item;
            if (_last == _array.Length) _last = 0;
            Size++;     
        }

        public T Dequeue()
        {
            if(IsEmpty)
                throw new InvalidOperationException("ResizingrrayQueue underflow.");

            Debug.Assert(_array != null, "_array != null");
            var item = _array[_first];
            _array[_first] = default(T);
            Size--;
            _first++;
            if (_first == _array.Length) _first = 0;
            if (Size > 0 && Size == _array.Length / 4) Resize(_array.Length / 2);
            return item;
        }

        public T Peek()
        {
            if (IsEmpty)
                throw new InvalidOperationException("ResizingrrayQueue underflow.");

            Debug.Assert(_array != null, "_array != null");
            return _array[_first];
        }
        #endregion

        private void Resize(int capacity)
        {
            if (capacity < Size)
            {
                throw new ArgumentException("value should be greater than Size.", nameof(capacity));
            }

            var tempArray = new T[capacity];

            Debug.Assert(_array != null, "_array != null");
            for (var i = 0; i < Size; i++)
            {
                tempArray[i] = _array[(i + _first) % _array.Length];
            }

            _array = tempArray;

            _first = 0;
            _last = Size;
        }
    }
}
