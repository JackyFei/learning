using System;

namespace SortingLibrary
{
    internal static class SortHelper
    {
        public static bool Less<T>(T obj1, T obj2) where T : IComparable<T>
        {
            return obj1.CompareTo(obj2) < 0;
        }

        public static bool Greater<T>(T obj1, T obj2) where T : IComparable<T>
        {
            return obj1.CompareTo(obj2) > 0;
        }

        public static int Compare<T>(T obj1, T obj2) where T : IComparable<T>
        {
            return obj1.CompareTo(obj2);
        }

        public static void Exch<T>(T[] objArray, int i, int j)
        {
            var swap = objArray[i];
            objArray[i] = objArray[j];
            objArray[j] = swap;
        }

        public static bool IsSorted<T>(T[] objArray, SortOrder sortOrder = SortOrder.ASC) where T : IComparable<T>
        {
            var len = objArray.Length;
            for (var i = 1; i < len; i++)
            {
                if (sortOrder == SortOrder.ASC && Greater(objArray[i - 1], objArray[i]))
                    return false;
                if (sortOrder == SortOrder.DESC && Less(objArray[i - 1], objArray[i]))
                    return false;
            }
            return true;
        }

        public static void Shuffle<T>(T[] a)
        {
            var seed = DateTime.Now.Millisecond;
            var random = new Random(seed);
            var n = a.Length;
            for (var i = 0; i < n; i++)
            {
                // choose index uniformly in [i, n-1]
                var r = i + (int) (random.NextDouble() * (n - i));
                var swap = a[r];
                a[r] = a[i];
                a[i] = swap;
            }
        }

        /// <summary>
        /// Insertion sort for a subarray.
        /// </summary>
        public static void InsertionSort<T>(T[] objArray, int low, int high, SortOrder sortOrder) where T : IComparable<T>
        {
            for (var i = low + 1; i <= high; i++)
            {
                for (var j = i; j > low; j--)
                {
                    if (sortOrder == SortOrder.ASC && Less(objArray[j], objArray[j - 1])
                        || sortOrder == SortOrder.DESC && Greater(objArray[j], objArray[j - 1]))
                        Exch(objArray, j, j - 1);
                }
            }
        }
    }
}
