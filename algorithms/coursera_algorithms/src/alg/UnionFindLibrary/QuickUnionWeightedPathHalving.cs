namespace UnionFindLibrary
{
    /// <summary>
    /// This implementation uses weighted quick union (by size) with path compaction(via halving). 
    /// Initializing a data structure with n sites takes linear time.
    /// Afterwards, <see cref="QuickUnionWeighted.Union"/>, <see cref="QuickUnionWeighted.Find"/>, and <see cref="QuickUnionWeighted.Connected"/> take logarithmic time (in the worst case) and <see cref="UnionFindBase.Count"/> takes constant time.
    /// Moreover, the amortized time per <see cref="QuickUnionWeighted.Union"/>,<see cref="QuickUnionWeighted.Find"/>, and <see cref="QuickUnionWeighted.Connected"/> operation has inverse Ackermann complexity.
    /// </summary>
    public class QuickUnionWeightedPathHalving : QuickUnionWeighted
    {
        public QuickUnionWeightedPathHalving(int n) : base(n) { }

        public override int Find(int p)
        {
            while (p != Parent[p])
            {
                Parent[p] = Parent[Parent[p]];
                p = Parent[p];
            }
            return p;
        }
    }
}
