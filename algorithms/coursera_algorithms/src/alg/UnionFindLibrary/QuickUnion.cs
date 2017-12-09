using System.Diagnostics;

namespace UnionFindLibrary
{
    /// <summary>
    /// This implementation uses quick union.
    /// Initializing a data structure with<em>n</em> sites takes linear time.
    /// Afterwards, the <see cref="Union"/>, <see cref="Find"/>, <see cref="Connected"/> operations take linear time (in the worst case) and the <see cref="UnionFindBase.Count"/> operation takes constant time.
    /// </summary>
    public class QuickUnion : UnionFindBase
    {
        public QuickUnion(int n) : base(n) { }

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
            Debug.Assert(Parent != null, "Parent != null");
            Parent[i] = j;
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
            while (p != Parent[p]) p = Parent[p];
            return p;
        }
    }
}
