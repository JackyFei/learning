using System;
using SortingLibrary.PriorityQueues;

namespace SortingLibrary
{
    /// <summary>
    /// We can use any priority queue to develop a sorting method.
    /// We insert all the keys to be sorted into a minimum-oriented priority queue, then repeatedly use remove the minimum to remove them all in order.
    /// When using a heap for the priority queue, we obtain heapsort.
    /// Heap construction.
    /// We can accomplish this task in time proportional to N lg N, by proceeding from left to right through the array, using swim() to ensure that the entries to the left of the scanning pointer make up a heap-ordered complete tree, like successive priority queue insertions. 
    /// A clever method that is much more efficient is to proceed from right to left, using sink() to make subheaps as we go.
    /// Every position in the array is the root of a small subheap; sink() works or such subheaps, as well. If the two children of a node are heaps, then calling sink() on that node makes the subtree rooted there a heap.
    /// Sortdown.
    /// Most of the work during heapsort is done during the second phase, where we remove the largest remaining items from the heap and put it into the array position vacated as the heap shrinks.
    /// Proposition.
    /// Sink-based heap construction is linear time.
    /// Proposition.
    /// Heapsort users fewer than 2n lg n compare and exchanges to sort n items.
    /// Most items reinserted into the heap during sortdown go all the way to the bottom.
    /// We can thus save time by avoiding the check for whether the item has reached its position, simply promoting the larger of the two children until the bottom is reached, then moving back up the heap to the proper position.
    /// This idea cuts the number of compares by a factor of 2 at the expense of extra bookkeeping.
    /// </summary>
    public class HeapSort : ISort
    {
        public void Sort<T>(T[] objArray, SortOrder sortOrder = SortOrder.ASC) where T : IComparable<T>
        {
            var n = objArray.Length;
            var k = Convert.ToInt32(Math.Floor(n / 2d)) - 1;
            for (;k >= 0; k--)
                Sink(objArray, k, n, sortOrder);
            while (n > 0)
            {
                SortHelper.Exch(objArray, 0, --n);
                Sink(objArray, 0, n, sortOrder);
            }
        }

        /// <summary>
        /// Top-down heapify (sink).
        /// In <see cref="BinaryHeapPriorityQueue{T}"/>, the array index is from 1 to n. But for sort method, we use 0 to n-1, so we could sort the array in place.
        /// </summary>
        private static void Sink<T>(T[] objArray, int k, int n, SortOrder sortOrder) where T : IComparable<T>
        {
            while (2 * k + 1 <= n - 1)
            {
                var j = 2 * k + 1;
                if (j <= n - 2 && (sortOrder == SortOrder.ASC && SortHelper.Less(objArray[j], objArray[j + 1]) || sortOrder == SortOrder.DESC && SortHelper.Greater(objArray[j], objArray[j + 1]))) j++;
                if (sortOrder == SortOrder.ASC && SortHelper.Greater(objArray[k], objArray[j]) || sortOrder == SortOrder.DESC && SortHelper.Less(objArray[k], objArray[j])) break;
                SortHelper.Exch(objArray, k, j);
                k = j;
            }
        }
    }
}
