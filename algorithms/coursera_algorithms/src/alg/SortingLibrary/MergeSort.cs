using System;

namespace SortingLibrary
{
    /// <summary>
    /// Top-down mergesort, combining two ordered arrays to make one larger ordered array.
    /// </summary>
    public class MergeSort : ISort
    {
        public void Sort<T>(T[] objArray, SortOrder sortOrder = SortOrder.ASC) where T : IComparable<T>
        {
            var copy = new T[objArray.Length];
            Sort(objArray, copy, 0, objArray.Length - 1, sortOrder);
        }

        /// <summary>
        /// Stably merge a[low .. mid] with a[mid+1 ..high] using aux[low .. high].
        /// a[low..mid] and a[mid+1..high] should be orderd.
        /// </summary>
        private static void Merge<T>(T[] a, T[] copy, int low, int mid, int high, SortOrder sortOrder) where T : IComparable<T>
        {
            // copy to another array and then merge back to a.
            for (var k = low; k <= high; k++)
            {
                copy[k] = a[k];
            }

            var i = low;
            var j = mid + 1;

            for (var k = low; k <= high; k++)
            {
                // if no item at left, then just need to copy right items.
                if (i > mid) a[k] = copy[j++];
                // if no item at right, then just need to copy left items
                else if (j > high) a[k] = copy[i++];
                // compare the items from left and right, then select one and copy to a.
                else if ((sortOrder == SortOrder.ASC && SortHelper.Less(copy[j], copy[i])) || (sortOrder == SortOrder.DESC && SortHelper.Greater(copy[j], copy[i]))) a[k] = copy[j++];
                else a[k] = copy[i++];
            }
        }

        /// <summary>
        /// Recursive Sort and merge.
        /// </summary>
        private static void Sort<T>(T[] a, T[] copy, int low, int high, SortOrder sortOrder) where T : IComparable<T>
        {
            if (low >= high) return;
            int mid = low + (high - low) / 2;
            Sort(a, copy, low, mid, sortOrder);
            Sort(a, copy, mid + 1, high, sortOrder);
            Merge(a, copy, low, mid, high, sortOrder);
        }
    }
}
