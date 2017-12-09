using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace FundamentalLibrary.Common
{
    internal class ListEnumerator<T> : IEnumerator<T>
    {
        private Node<T> _current;

        public ListEnumerator(Node<T> first)
        {
            _current = new Node<T>(default(T), first);
        }

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            Debug.Assert(_current != null, "_current != null");
            if (_current.Next == null)
                return false;
            _current = _current.Next;
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
                Debug.Assert(_current != null, "_current != null");
                return _current.Item;
            }
        }

        object IEnumerator.Current => Current;
    }
}
