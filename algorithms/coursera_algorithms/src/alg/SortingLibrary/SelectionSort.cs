using System;

namespace SortingLibrary
{
    /// <summary>
    /// Selection sort.
    /// Selection sort uses ~n2/2 compares and n exchanges to sort an array of length n.
    /// </summary>
    public class SelectionSort : ISort
    {
        public void Sort<T>(T[] objArray, SortOrder sortOrder = SortOrder.ASC) where T : IComparable<T>
        {
            var len = objArray.Length;
            for (var i = 0; i < len; i++)
            {
                var minOrMax = i;
                for (var j = i + 1; j < len; j++)
                {
                    if (sortOrder == SortOrder.ASC)
                    {
                        if (SortHelper.Greater(objArray[minOrMax], objArray[j]))
                        {
                            minOrMax = j;
                        }
                    }
                    else
                    {
                        if (SortHelper.Less(objArray[minOrMax], objArray[j]))
                        {
                            minOrMax = j;
                        }
                    }
                }
                SortHelper.Exch(objArray, i, minOrMax);
            }
        }
    }
}
