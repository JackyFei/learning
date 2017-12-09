namespace UnionFindLibrary
{
    /// <summary>
    /// The <see cref="IUnionFind"/> interface represents a union–find data type (also known as the disjoint-sets data type).
    /// It supports the <see cref="Union(int, int)"/> and <see cref="Find(int)"/> operations, along with a <see cref="Connected(int, int)"/> operation for determining whether two sites are in the same component and a <see cref="Count"/> operation that returns the total number of components.
    /// </summary>
    public interface IUnionFind
    {
        /// <summary>
        /// Merges the component containing site p with the the component containing site q.
        /// </summary>
        void Union(int p, int q);

        /// <summary>
        /// Returns true if the the two sites are in the same component.
        /// </summary>
        bool Connected(int p, int q);

        /// <summary>
        /// Returns the component identifier for the component containing site p.
        /// </summary>
        int Find(int p);

        /// <summary>
        /// Returns the number of components.
        /// </summary>
        int Count { get; }
    }
}
