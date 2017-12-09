using System;

namespace SortingLibrary
{
    /// <summary>
    /// Quick sort using 3 way partitioning.
    /// Arrays with large numbers of duplicate sort keys arise frequently in applications.
    /// In such applications, there is potential to reduce the time of the sort from linearithmic to linear.
    /// One straightforward idea is to partition the array into three parts, one each for items with keys smaller than, equal to, and larger than the partitioning item's key.
    /// Quicksort with 3-way partitioning is entropy-optimal.
    /// </summary>
    public class Quick3WaySort : ISort
    {
        public void Sort<T>(T[] objArray, SortOrder sortOrder = SortOrder.ASC) where T : IComparable<T>
        {
            SortHelper.Shuffle(objArray);

            Sort(objArray, 0, objArray.Length - 1, sortOrder);
        }

        private static void Sort<T>(T[] objArray, int low, int high, SortOrder sortOrder) where T : IComparable<T>
        {
            if (high <= low) return;

            var ltAndGt = Partition(objArray, low, high, sortOrder);
            var lt = ltAndGt.Item1;
            var gt = ltAndGt.Item2;
            // a[lo..lt-1] < v = a[lt..gt] < a[gt+1..hi]. 
            Sort(objArray, low, lt - 1, sortOrder);
            Sort(objArray, gt + 1, high, sortOrder);
        }

        private static Tuple<int, int> Partition<T>(T[] objArray, int low, int high, SortOrder sortOrder) where T : IComparable<T>
        {
            var lt = low;
            var gt = high;
            var v = objArray[low];
            var i = low;
            while (i <= gt)
            {
                var cmp = SortHelper.Compare(objArray[i], v);

                if (sortOrder == SortOrder.ASC && cmp < 0 || sortOrder == SortOrder.DESC && cmp > 0) SortHelper.Exch(objArray, lt++, i++);
                else if (sortOrder == SortOrder.ASC && cmp > 0 || sortOrder == SortOrder.DESC && cmp < 0) SortHelper.Exch(objArray, i, gt--);
                else i++;
            }
            return new Tuple<int, int>(lt, gt);
        }
    }
}
