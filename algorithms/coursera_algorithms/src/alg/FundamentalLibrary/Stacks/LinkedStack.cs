using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using FundamentalLibrary.Common;

namespace FundamentalLibrary.Stacks
{
    /// <summary>
    /// The <see cref="LinkedStack{T}"/> class represents a last-in-first-out (LIFO) stack of generic items.
    /// This implementation uses a singly-linked list with a nested class for linked-list nodes.
    /// The <see cref="Push"/>, <see cref="Pop"/>, <see cref="Peek"/>, <see cref="Size"/>, and <see cref="IsEmpty"/> operations all take constant time in the worst case.
    /// </summary>
    public class LinkedStack<T> : IStack<T>
    {
        private Node<T> _first;

        public LinkedStack()
        {
            _first = null;
            Size = 0;
        }

        #region IStack<T>
        public bool IsEmpty => _first == null;

        public int Size { get; private set; }

        public void Push(T item)
        {
            var oldFirst = _first;
            _first = new Node<T>(item, oldFirst);
            Size++;
        }

        public T Pop()
        {
            if(IsEmpty) throw new InvalidOperationException("LinkedStack underflow.");
            Debug.Assert(_first != null, "_first != null");
            var item = _first.Item;
            _first = _first.Next;
            Size--;
            return item;
        }

        public T Peek()
        {
            if (IsEmpty) throw new InvalidOperationException("LinkedStack underflow.");
            Debug.Assert(_first != null, "_first != null");
            return _first.Item;
        }

        #endregion

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
    }
}
