using System.Diagnostics;

namespace UnionFindLibrary
{
    /// <summary>
    /// This implementation uses weighted quick union by size (without path compression).
    /// Initializing a data structure n sites takes linear time.
    /// Afterwards, the <see cref="Union"/>, <see cref="Find"/>, and <see cref="Connected"/> operations take logarithmic time (in the worst case) and the <see cref="UnionFindBase.Count"/> operation takes constant time.
    /// </summary>
    public class QuickUnionWeighted : UnionFindBase
    {
        // size array, maintain the size of tree.
        private readonly int[] _size;
        public QuickUnionWeighted(int n) : base(n)
        {
            _size = new int[n];
            for(var i = 0; i < _size.Length; i++)
            {
                _size[i] = 1;
            }
        }

        public override bool Connected(int p, int q)
        {
            ValidateArguments(p, q);
            return Root(p) == Root(q);
        }

        public override void Union(int p, int q)
        {
            ValidateArguments(p, q);

            var i = Root(p);
            var j = Root(q);

            // if they have same root, just do nothing.
            if (i == j) return;

            // check which one has less objects, and set the less root, and update the sz array.
            Debug.Assert(_size != null, "_size != null");
            Debug.Assert(Parent != null, "Parent != null");
            if (_size[i] < _size[j])
            {             
                Parent[i] = j;
                _size[j] += _size[i];
            }
            else
            {
                Parent[j] = i;
                _size[i] += _size[j];
            }
            Count--;
        }

        public override int Find(int p)
        {
            ValidateArguments(p, null);
            return Root(p);
        }

        protected virtual int Root(int p)
        {
            Debug.Assert(Parent != null, "Parent != null");
            while (p != Parent[p])
            {
                p = Parent[p];
            }
            return p;
        }
    }
}
