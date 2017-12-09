using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using FundamentalLibrary.Common;

namespace FundamentalLibrary.Queues
{    
    /// <summary>
    /// This implementation uses a singly-linked list with a non-static nested class for linked-list nodes.
    /// The <see cref="Enqueue"/>, <see cref="Dequeue"/>, <see cref="Peek"/>, <see cref="Size"/>, and <see cref="IsEmpty"/>
    /// operations all take constant time in the worst case.
    /// </summary>
    public class LinkedQueue<T> : IQueue<T>
    {
        private Node<T> _first;
        private Node<T> _last;

        public LinkedQueue()
        {
            _first = null;
            _last = null;
            Size = 0;
        }

        #region IEnumerable<T>
        public IEnumerator<T> GetEnumerator()
        {
            return new ListEnumerator<T>(_first);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region IQueue<T>

        public bool IsEmpty => _first == null;

        public int Size { get; private set; }

        public void Enqueue(T item)
        {
            var oldLast = _last;
            _last = new Node<T>(item, null);
            if (IsEmpty)
            {
                _first = _last;
            }
            else
            {
                Debug.Assert(oldLast != null, "oldLast != null");
                oldLast.Next = _last;
            }
            Size++;
        }

        public T Dequeue()
        {
            if (IsEmpty)
                throw new InvalidOperationException("LinkedQueue underflow.");

            Debug.Assert(_first != null, "_first != null");
            var item = _first.Item;
            _first = _first.Next;
            Size--;
            if (IsEmpty) _last = null;
            return item;
        }

        public T Peek()
        {
            if (IsEmpty)
                throw new InvalidOperationException("LinkedQueue underflow.");

            Debug.Assert(_first != null, "_first != null");
            return _first.Item;
        }
        #endregion
    }
}
