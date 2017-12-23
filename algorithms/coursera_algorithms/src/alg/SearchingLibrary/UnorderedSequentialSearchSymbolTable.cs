using System;
using System.Collections;
using System.Collections.Generic;

namespace SearchingLibrary
{
    public class UnorderedSequentialSearchSymbolTable<TKey, TValue> : ISymbolTable<TKey, TValue> where TKey : IComparable<TKey>
    {
        private Node _first = null;

        #region ISymbolTable
        public bool IsEmpty
        {
            get { return Size == 0; }
        }

        public int Size { get; set; }

        public IEnumerable<TKey> Keys
        {
            get
            {
                return new KeyEnumerable(_first);
            }
        }

        public void Put(TKey key, TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var current = _first;
            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    current.Value = value;
                    return;
                }
                current = current.Next;
            }
            // no key found, then just add a new node as first node.
            _first = new Node(key, value, _first);
            Size++;
        }

        public bool TryGet(TKey key, out TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var current = _first;
            while (current != null)
            {
                if (current.Key.CompareTo(key) == 0)
                {
                    value = current.Value;
                    return true;
                }
                current = current.Next;
            }

            // no key is found.
            value = default(TValue);
            return false;
        }

        public bool Contains(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var current = _first;
            while (current != null)
            {
                if (current.Key.CompareTo(key) == 0)
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public void Delete(TKey key)
        {
            if(key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            _first = Delete(_first, key);
        }

        public void DeleteMax()
        {
            if(TryGetMax(out TKey key))
            {
                Delete(key);
            }
        }

        public void DeleteMin()
        {
            if(TryGetMin(out TKey key))
            {
                Delete(key);
            }
        }

        public bool TryGetFloor(TKey key, out TKey keyResult)
        {
            var current = _first;
            object keyFloor = null;
            while (current != null)
            {
                if (current.Key.CompareTo(key) < 0)
                {
                    if (keyFloor == null || current.Key.CompareTo((TKey)keyFloor) > 0)
                    {
                        keyFloor = current.Key;
                    }
                }
                current = current.Next;
            }

            if(keyFloor == null)
            {
                keyResult = default(TKey);
                return false;
            }
            else
            {
                keyResult = (TKey)keyFloor;
                return true;
            }
        }

        public bool TryGetCeiling(TKey key, out TKey keyResult)
        {
            var current = _first;
            object keyCeiling = null;
            while (current != null)
            {
                if (current.Key.CompareTo(key) > 0)
                {
                    if (keyCeiling == null || current.Key.CompareTo((TKey)keyCeiling) < 0)
                    {
                        keyCeiling = current.Key;
                    }
                }
                current = current.Next;
            }

            if (keyCeiling == null)
            {
                keyResult = default(TKey);
                return false;
            }
            else
            {
                keyResult = (TKey)keyCeiling;
                return true;
            }
        }

        public bool TryGetMax(out TKey key)
        {
            if(_first == null)
            {
                key = default(TKey);
                return false;
            }

            var current = _first;
            var keyMax = current.Key;
            while(current != null)
            {
                if(current.Key.CompareTo(keyMax) > 0)
                {
                    keyMax = current.Key;
                }
                current = current.Next;
            }

            key = keyMax;
            return true;
        }

        public bool TryGetMin(out TKey key)
        {
            if (_first == null)
            {
                key = default(TKey);
                return false;
            }

            var current = _first;
            var keyMin = current.Key;
            while (current != null)
            {
                if (current.Key.CompareTo(keyMin) < 0)
                {
                    keyMin = current.Key;
                }
                current = current.Next;
            }

            key = keyMin;
            return true;
        }

        #endregion

        #region Inner Classes
        private class Node
        {
            public TKey Key { get; }
            public TValue Value { get; set; }
            public Node Next { get; set; }

            public Node(TKey key, TValue val, Node next)
            {
                this.Key = key;
                this.Value = val;
                this.Next = next;
            }
        }

        private class KeyEnumerable : IEnumerable<TKey>
        {
            private Node _first;

            public KeyEnumerable(Node first)
            {
                _first = first;
            }
            public IEnumerator<TKey> GetEnumerator()
            {
                return new KeyEnumerator(_first);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new KeyEnumerator(_first);
            }
        }

        private class KeyEnumerator : IEnumerator<TKey>
        {
            private Node _current;
            private Node _first;

            public KeyEnumerator(Node first)
            {
                _first = first;
                // set a dummy node to point to the _first node.
                _current = new Node(default(TKey), default(TValue), _first);
            }

            public TKey Current
            {
                get
                {
                    if (_current == null)
                        throw new InvalidOperationException("_current is null.");
                    return _current.Key;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public void Dispose()
            {
                
            }

            public bool MoveNext()
            {
                if (_current.Next == null)
                {
                    return false;
                }
                _current = _current.Next;
                return true;
            }

            public void Reset()
            {
                _current = _first;
            }
        }
        #endregion

        private Node Delete(Node x, TKey key)
        {
            if (x == null) return null;
            if (key.Equals(x.Key))
            {
                Size--;
                return x.Next;
            }
            x.Next = Delete(x.Next, key);
            return x;
        }
    }
}
