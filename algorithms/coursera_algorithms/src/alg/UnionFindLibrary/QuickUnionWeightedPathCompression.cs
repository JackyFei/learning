namespace UnionFindLibrary
{
    /// <summary>
    /// This implementation uses weighted quick union (by size) with full path compression.
    /// Initializing a data structure with<em>n</em> sites takes linear time.
    /// Afterwards, <see cref="QuickUnionWeighted.Union"/>, <see cref="QuickUnionWeighted.Find"/>, and <see cref="QuickUnionWeighted.Connected"/> take logarithmic time (in the worst case) and <see cref="UnionFindBase.Count"/> takes constant time.
    /// Moreover, the amortized time per <see cref="QuickUnionWeighted.Union"/>, <see cref="QuickUnionWeighted.Find"/>, and <see cref="QuickUnionWeighted.Connected"/> operation has inverse Ackermann complexity.
    /// </summary>
    public class QuickUnionWeightedPathCompression : QuickUnionWeighted
    {
        public QuickUnionWeightedPathCompression(int n) : base(n)
        {
        }

        protected override int Root(int p)
        {
            var root = p;
            while (root != Parent[p])
            {
                root = Parent[p];
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
