using System;

namespace SortingLibrary
{
    /// <summary>
    /// Insertion sort.
    /// This implementation makes ~ 1/2 n^2 compares and exchanges in the worst case, so it is not suitable for sorting large arbitrary arrays.
    /// More precisely, the number of exchanges is exactly equal to the number of inversions.
    /// So, for example, it sorts a partially-sorted array in linear time.
    /// The sorting algorithm is stable and uses O(1) extra memory.
    /// </summary>
    public class InsertionSort : ISort
    {
        public void Sort<T>(T[] objArray, SortOrder sortOrder = SortOrder.ASC) where T : IComparable<T>
        {
            SortHelper.InsertionSort(objArray, 0, objArray.Length - 1, sortOrder);
        }
    }
}
