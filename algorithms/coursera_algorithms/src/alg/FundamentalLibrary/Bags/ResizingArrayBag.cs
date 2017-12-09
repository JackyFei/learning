using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace FundamentalLibrary.Bags
{
    /// <summary>
    /// This implementation uses a resizing array.
    /// The <see cref="Add"/> operation takes constant amortized time; 
    /// the <see cref="IsEmpty"/>, and <see cref="Size"/> operations take constant time. 
    /// Iteration takes time proportional to the number of items.
    /// </summary>
    public class ResizingArrayBag<T> : IBag<T>
    {
        private T[] _array;

        public ResizingArrayBag(int initialCapacity = 10)
        {
            if (initialCapacity < 1)
                throw new ArgumentException("value should be greater than zero.", nameof(initialCapacity));

            _array = new T[initialCapacity * 2];
            Size = 0;
        }

        #region IEnumerable<T>
        public IEnumerator<T> GetEnumerator()
        {
            return new ReverseArrayEnumerator(_array, Size);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class ReverseArrayEnumerator : IEnumerator<T>
        {
            private int _current;
            private readonly T[] _array;

            public ReverseArrayEnumerator(T[] array, int size)
            {
                _array = array;
                _current = size;
            }

            public void Dispose()
            {

            }

            public bool MoveNext()
            {
                if (_current <= 0) return false;
                _current--;
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
                    return _array[_current];
                }
            }

            object IEnumerator.Current => Current;
        }
        #endregion

        #region IBag<T>
        public int Size { get; private set; }
        public bool IsEmpty => Size == 0;
        public void Add(T item)
        {
            if (Size == _array.Length)
            {
                Resize(2 * _array.Length);
            }
            _array[Size++] = item;
        }
        #endregion

        private void Resize(int capacity)
        {
            Debug.Assert(capacity >= Size);

            var tempArray = new T[capacity];
            for (var i = 0; i < Size; i++)
            {
                tempArray[i] = _array[i];
            }

            _array = tempArray;
        }
    }
}
