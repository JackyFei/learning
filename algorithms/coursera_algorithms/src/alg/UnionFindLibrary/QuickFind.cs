using System.Diagnostics;

namespace UnionFindLibrary
{
    /// <summary>
    /// This implementation uses quick find.
    /// Initializing a data structure with n sites takes linear time.
    /// Afterwards, the <see cref="Find(int)"/>, <see cref="Connected(int, int)"/>, and <see cref="UnionFindBase.Count"/> operations take constant time but the <see cref="Union(int, int)"/> operation takes linear time.
    /// </summary>
    public class QuickFind : UnionFindBase
    {
        public QuickFind(int n) : base(n) { }

        public override void Union(int p, int q)
        {
            ValidateArguments(p, q);

            Debug.Assert(Parent != null, "Parent != null");
            var pid = Parent[p];
            var qid = Parent[q];
            for(var i = 0; i < Parent.Length; i++)
            {
                if(Parent[i] == pid)
                {
                    Parent[i] = qid;
                }
            }
            Count--;
        }

        public override bool Connected(int p, int q)
        {
            ValidateArguments(p, q);

            Debug.Assert(Parent != null, "Parent != null");
            return Parent[p] == Parent[q];
        }

        public override int Find(int p)
        {
            ValidateArguments(p, null);

            Debug.Assert(Parent != null, "Parent != null");
            return Parent[p];
        }
    }
}
