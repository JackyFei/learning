namespace UnionFindLibrary
{
    /// <summary>
    /// This implementation uses quick union (no weighting) with full path compression.
    /// Initializing a data structure with n sites takes linear time.
    /// Afterwards, <see cref="QuickUnion.Union"/>, <see cref="QuickUnion.Find"/>, and <see cref="QuickUnion.Connected"/> take logarithmic amortized time <see cref="UnionFindBase.Count"/> takes constant time.
    /// </summary>
    public class QuickUnionPathCompression : QuickUnionWeighted
    {
        public QuickUnionPathCompression(int n) : base(n)
        {
        }

        protected override int Root(int p)
        {
            var root = p;
            while (root != Parent[root])
            {
                root = Parent[root];
            }

            while (p != root)
            {
                var newP = Parent[p];
                Parent[p] = root;
                p = newP;
            }
            return root;
        }
    }
}
