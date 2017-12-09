using System;

namespace SortingLibrary
{
    /// <summary>
    /// Optimized version of merge sort.
    /// * Use insertion sort for small subarrays. 
    ///     We can improve most recursive algorithms by handling small cases differently. 
    ///     Switching to insertion sort for small subarrays will improve the running time of a typical mergesort implementation by 10 to 15 percent.
    /// * Test whether array is already in order. 
    ///     We can reduce the running time to be linear for arrays that are already in order by adding a test to skip call to merge() if a[mid] is less than or equal to a[mid+1]. 
    ///     With this change, we still do all the recursive calls, but the running time for any sorted subarray is linear.
    /// * Eliminate the copy to the auxiliary array. 
    ///     It is possible to eliminate the time (but not the space) taken to copy to the auxiliary array used for merging. 
    ///     To do so, we use two invocations of the sort method, one that takes its input from the given array and puts the sorted output in the auxiliary array; the other takes its input from the auxiliary array and puts the sorted output in the given array. 
    ///     With this approach, in a bit of mindbending recursive trickery, we can arrange the recursive calls such that the computation switches the roles of the input array and the auxiliary array at each level.
    /// </summary>
    public class MergeXSort : ISort
    {
        // cutoff to insertion sort.
        private readonly int _cutoff;

        public MergeXSort(int insertionSortCutoff = 8)
        {
            _cutoff = insertionSortCutoff;
        }

        public void Sort<T>(T[] objArray, SortOrder sortOrder = SortOrder.ASC) where T : IComparable<T>
        {
            var copy = objArray.Clone() as T[];
            Sort(copy, objArray, 0, objArray.Length - 1, sortOrder);
        }

        /// <summary>
        /// Stably merge a[low .. mid] with a[mid+1 ..high] using aux[low .. high].
        /// a[low..mid] and a[mid+1..high] should be orderd.
        /// </summary>
        private static void Merge<T>(T[] src, T[] dst, int low, int mid, int high, SortOrder sortOrder) where T : IComparable<T>
        {
            var i = low;
            var j = mid + 1;

            for (var k = low; k <= high; k++)
            {
                // if no item at left, then just need to copy right items.
                if (i > mid) dst[k] = src[j++];
                // if no item at right, then just need to copy left items
                else if (j > high) dst[k] = src[i++];
                // compare the items from left and right, then select one and copy to a.
                else if ((sortOrder == SortOrder.ASC && SortHelper.Less(src[j], src[i])) || (sortOrder == SortOrder.DESC && SortHelper.Greater(src[j], src[i]))) dst[k] = src[j++];
                else dst[k] = src[i++];
            }
        }

        /// <summary>
        /// Recursive Sort and merge.
        /// </summary>
        private void Sort<T>(T[] src, T[] dst, int low, int high, SortOrder sortOrder) where T : IComparable<T>
        {
            // if the length of subarray which need to be sorted is small, then just use insertation sort.
            if (high - low <= _cutoff)
            {
                SortHelper.InsertionSort(dst, low, high, sortOrder);
                return;
            }

            var mid = low + (high - low) / 2;
            Sort(dst, src, low, mid, sortOrder);
            Sort(dst, src, mid + 1, high, sortOrder);

            // before calling merge, check if the dst[mid] and dst[mid+1] is in order, if yes, then just copy to dst and return.
            if ((sortOrder == SortOrder.ASC && SortHelper.Less(src[mid], src[mid + 1]))
                || (sortOrder == SortOrder.DESC && SortHelper.Greater(src[mid], src[mid + 1])))
            {
                Array.Copy(src, low, dst, low, high - low + 1);
                return;
            }

            Merge(src, dst, low, mid, high, sortOrder);
        }
    }
}
