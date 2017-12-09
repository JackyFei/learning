using System.Collections;
using System.Collections.Generic;
using FundamentalLibrary.Common;

namespace FundamentalLibrary.Bags
{
    /// <summary>
    /// This implementation uses a singly-linked list with a non-static nested class Node.
    /// The <see cref="Add"/>, <see cref="IsEmpty"/>, and <see cref="Size"/> operations take constant time.
    /// Iteration takes time proportional to the number of items.
    /// </summary>
    public class LinkedBag<T> : IBag<T>
    {
        private Node<T> _first;

        public LinkedBag()
        {
            _first = null;
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

        #region IBag<T>
        public int Size { get; private set; }
        public bool IsEmpty => _first == null;

        public void Add(T item)
        {
            var oldFirst = _first;
            _first = new Node<T>(item, oldFirst);
            Size++;
        }
        #endregion
    }
}
