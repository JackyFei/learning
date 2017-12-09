using System;
using System.Diagnostics;

namespace UnionFindLibrary
{
    public abstract class UnionFindBase : IUnionFind
    {
        protected readonly int[] Parent;
        public int Count { get; protected set; }

        protected UnionFindBase(int n)
        {
            Parent = InitializeArray(n);
            Count = n;
        }

        public abstract void Union(int p, int q);

        public abstract bool Connected(int p, int q);

        public abstract int Find(int p);

        protected void ValidateArguments(int? p, int? q)
        {
            Debug.Assert(Parent != null, "Parent != null");
            if (p.HasValue && (p < 0 || p >= Parent.Length))
                throw new ArgumentOutOfRangeException(nameof(p));

            if (q.HasValue && (q < 0 || q >= Parent.Length))
                throw new ArgumentOutOfRangeException(nameof(q));
        }

        private static int[] InitializeArray(int n)
        {
            var idArray = new int[n];

            for (var i = 0; i < n; i++)
            {
                idArray[i] = i;
            }
            return idArray;
        }
    }
}
