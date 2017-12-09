using System;

namespace SortingLibrary
{
    public interface ISort
    {
        void Sort<T>(T[] objArray, SortOrder sortOrder = SortOrder.ASC) where T : IComparable<T>;
    }

    public enum SortOrder : byte
    {
        ASC = 0,
        DESC = 1
    }
}