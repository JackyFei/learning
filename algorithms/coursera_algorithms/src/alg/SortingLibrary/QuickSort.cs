using System;

namespace SortingLibrary
{
    /// <summary>
    /// Quicksort is a divide-and-conquer method for sorting. 
    /// It works by partitioning an array into two parts, then sorting the parts independently.
    /// It is in-place (uses only a small auxiliary stack), requires time proportional to N log N on the average to sort N items, and has an extremely short inner loop.
    /// Quicksort uses ~2 N ln N compares (and one-sixth that many exchanges) on the average to sort an array of length N with distinct keys.
    /// Quicksort uses ~N2/2 compares in the worst case, but random shuffling protects against this case.
    /// </summary>
    public class QuickSort : ISort
    {
        public void Sort<T>(T[] objArray, SortOrder sortOrder = SortOrder.ASC) where T : IComparable<T>
        {
            SortHelper.Shuffle(objArray);
            
            Sort(objArray, 0, objArray.Length - 1, sortOrder);
        }

        /// <summary>
        /// Partition the subarray a[lo..hi] so that a[lo..j-1] less than or = a[j] less than or = a[j+1..hi] and return the index j.
        /// </summary>
        private static int Partition<T>(T[] objArray, int low, int high, SortOrder sortOrder) where T:IComparable<T>
        {
            var i = low;
            var j = high + 1;
            var v = objArray[low];
            while (true)
            {

                // find item on low to swap
                while (sortOrder == SortOrder.ASC && SortHelper.Less(objArray[++i], v) || sortOrder == SortOrder.DESC && SortHelper.Greater(objArray[++i], v))
                    if (i == high) break;

                // find item on high to swap
                while (sortOrder == SortOrder.ASC && SortHelper.Less(v, objArray[--j]) || sortOrder == SortOrder.DESC && SortHelper.Greater(v, objArray[--j]))
                    if (j == low) break;// redundant since a[lo] acts as sentinel

                // check if pointers cross
                if (i >= j) break;

                SortHelper.Exch(objArray, i, j);
            }

            // put partitioning item v at a[j]
            SortHelper.Exch(objArray, low, j);

            //now, a[lo..j - 1] <= a[j] <= a[j + 1..hi]
            return j;
        }

        private static void Sort<T>(T[] objArray, int low, int high, SortOrder sortOrder) where T:IComparable<T>
        {
            if (high <= low) return;
            var j = Partition(objArray, low, high, sortOrder);
            Sort(objArray, low, j - 1, sortOrder);
            Sort(objArray, j + 1, high, sortOrder);
        }
    }
}
