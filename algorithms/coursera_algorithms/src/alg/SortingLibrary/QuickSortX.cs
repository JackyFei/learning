using System;

namespace SortingLibrary
{
    /// <summary>
    /// Fast three-way partitioning. (J. Bentley and D. McIlroy).
    /// Uses the Bentley-McIlroy 3-way partitioning scheme, chooses the partitioning element using Tukey's ninther, and cuts off to insertion sort.
    /// Implement an entropy-optimal sort based on keeping equal keys at both the left and right ends of the subarray. 
    /// Maintain indices p and q such that a[low..p-1] that a[q+1..high] are all equal to a[low], an index i such that a[p..i-1] are all less than a[low] and an index j such that a[j+1..q] are all greater than a[low].
    /// Add to the inner partitioning loop code to swap a[i] with a[p] (and increment p) if it is equal to v and to swap a[j] with a[q] (and decrement q) if it is equal to v before the usual comparisons of a[i] and a[j] with v.
    /// After the partitioning loop has terminated, add code to swap the equal keys into position.
    /// </summary>
    public class QuickSortX : ISort
    {
        // cutoff to insertion sort, must be >= 1
        private readonly int _insertionSortCutoff;

        // cutoff to median-of-3 partitioning
        private readonly int _medianOf3Cutoff;

        public QuickSortX(int insertionSortCutoff = 8, int medianOf3Cutoff = 40)
        {
            _insertionSortCutoff = insertionSortCutoff;
            _medianOf3Cutoff = medianOf3Cutoff;
        }

        public void Sort<T>(T[] objArray, SortOrder sortOrder = SortOrder.ASC) where T : IComparable<T>
        {
            Sort(objArray, 0, objArray.Length - 1, sortOrder);
        }

        private void Sort<T>(T[] a, int low, int high, SortOrder sortOrder) where T : IComparable<T>
        {
            var n = high - low + 1;

            // cutoff to insertion sort
            if (n <= _insertionSortCutoff)
            {
                SortHelper.InsertionSort(a, low, high, sortOrder);
                return;
            }

            // use median-of-3 as partitioning element
            if (n <= _medianOf3Cutoff)
            {
                var m = Median3(a, low, low + n / 2, high);
                SortHelper.Exch(a, m, low);
            }
            else
            // use Tukey ninther as partitioning element
            {
                var eps = n / 8;
                var mid = low + n / 2;
                var m1 = Median3(a, low, low + eps, low + eps + eps);
                var m2 = Median3(a, mid - eps, mid, mid + eps);
                var m3 = Median3(a, high - eps - eps, high - eps, high);
                var ninther = Median3(a, m1, m2, m3);
                SortHelper.Exch(a, ninther, low);
            }

            // Bentley-McIlroy 3-way partitioning
            int i = low, j = high + 1;
            int p = low, q = high + 1;
            T v = a[low];
            while (true)
            {
                while (sortOrder == SortOrder.ASC && SortHelper.Less(a[++i], v) || sortOrder == SortOrder.DESC && SortHelper.Greater(a[++i], v))
                    if (i == high) break;
                while (sortOrder == SortOrder.ASC && SortHelper.Less(v, a[--j]) || sortOrder == SortOrder.DESC && SortHelper.Greater(v, a[--j]))
                    if (j == low) break;

                // pointers cross
                if (i == j && SortHelper.Compare(a[i], v) == 0)
                    SortHelper.Exch(a, ++p, i);
                if (i >= j) break;

                SortHelper.Exch(a, i, j);
                if (SortHelper.Compare(a[i], v) == 0) SortHelper.Exch(a, ++p, i);
                if (SortHelper.Compare(a[j], v) == 0) SortHelper.Exch(a, --q, j);
            }


            i = j + 1;
            for (int k = low; k <= p; k++)
                SortHelper.Exch(a, k, j--);
            for (int k = high; k >= q; k--)
                SortHelper.Exch(a, k, i++);

            Sort(a, low, j, sortOrder);
            Sort(a, i, high, sortOrder);
        }

        // return the index of the median element among a[i], a[j], and a[k]
        private static int Median3<T>(T[] a, int i, int j, int k) where T : IComparable<T>
        {
            return SortHelper.Less(a[i], a[j])
                ? (SortHelper.Less(a[j], a[k]) ? j : SortHelper.Less(a[i], a[k]) ? k : i)
                : (SortHelper.Less(a[k], a[j]) ? j : SortHelper.Less(a[k], a[i]) ? k : i);
        }
    }
}
