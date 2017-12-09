using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace FundamentalLibrary.Stacks
{
    /// <summary>
    /// The <see cref="ResizingArrayStack{T}"/> class represents a last-in-first-out (LIFO) stack of generic items.
    /// This implementation uses a resizing array, which double the underlying array when it is full and halves the underlying array when it is one-quarter full.
    /// The <see cref="Push"/>, <see cref="Pop"/> operations take constant amortized time.
    /// The <see cref="Peek"/>, <see cref="Size"/>, and <see cref="IsEmpty"/> operations take constant time in the worst case.
    /// </summary>
    public class ResizingArrayStack<T> : IStack<T>
    {
        private T[] _array;

        public ResizingArrayStack(int initialCapacity = 10)
        {
            if (initialCapacity < 1)
                throw new ArgumentException("value should be greater than zero.", nameof(initialCapacity));
            _array = new T[2 * initialCapacity];
            Size = 0;
        }

        #region IStack<T>

        public bool IsEmpty => Size == 0;

        public int Size { get; private set; }

        public void Push(T item)
        {
            Debug.Assert(_array != null, "_array != null");
            if (Size == _array.Length)
            {
                Resize(2 * _array.Length);
            }

            Debug.Assert(_array != null, "_array != null");
            _array[Size++] = item;
        }

        public T Pop()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("ResizingArrayStack underflow.");
            }

            Debug.Assert(_array != null, "_array != null");
            var item = _array[Size - 1];
            _array[Size - 1] = default(T);
            Size--;
            if (Size > 0 && Size == _array.Length / 4)
            {
                Resize(_array.Length / 2);
            }
            return item;
        }

        public T Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("ResizingArrayStack underflow.");
            }

            Debug.Assert(_array != null, "_array != null");
            return _array[Size - 1];
        }
        #endregion

        #region IEnumerator<T>
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
                tempArray[i] = _array[i];
            }

            _array = tempArray;
        }
    }
}
