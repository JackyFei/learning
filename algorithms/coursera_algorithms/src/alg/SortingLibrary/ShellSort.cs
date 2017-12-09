using System;

namespace SortingLibrary
{
    /// <summary>
    /// Shellsort is a simple extension of insertion sort that gains speed by allowing exchanges of entries that are far apart, to produce partially sorted arrays that can be efficiently sorted, eventually by insertion sort.
    /// The idea is to rearrange the array to give it the property that taking every hth entry (starting anywhere) yields a sorted sequence.
    /// Such an array is said to be h-sorted.
    /// By h-sorting for some large values of h, we can move entries in the array long distances and thus make it easier to h-sort for smaller values of h.
    /// Using such a procedure for any increment sequence of values of h that ends in 1 will produce a sorted array.
    /// </summary>
    public class ShellSort : ISort
    {
        public void Sort<T>(T[] objArray, SortOrder sortOrder = SortOrder.ASC) where T : IComparable<T>
        {
            var len = objArray.Length;
            var h = ComputeH(len);

            while (h >= 1)
            {
                for (var i = h; i < len; i++)
                {
                    for (var j = i; j >= h; j = j - h)
                    {
                        if (sortOrder == SortOrder.ASC)
                        {
                            if (SortHelper.Less(objArray[j], objArray[j - h]))
                            {
                                SortHelper.Exch(objArray, j, j - h);
                            }
                        }
                        else
                        {
                            if (SortHelper.Greater(objArray[j], objArray[j - h]))
                            {
                                SortHelper.Exch(objArray, j, j - h);
                            }
                        }
                    }
                }

                h = h / 3;
            }
        }

        /// <summary>
        /// 3x+1 increment sequence:  1, 4, 13, 40, 121, 364, 1093, ..
        /// </summary>
        private static int ComputeH(int len)
        {
            var h = 1;
            var n = len / 3;
            while (h < n) h = 3 * h + 1;
            return h;
        }
    }
}
