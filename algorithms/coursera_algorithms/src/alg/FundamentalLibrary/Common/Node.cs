
namespace FundamentalLibrary.Common
{
    /// <summary>
    /// Linked list node
    /// </summary>
    internal class Node<T>
    {
        public Node(T item, Node<T> next)
        {
            Item = item;
            Next = next;
        }

        public T Item;
        public Node<T> Next;
    }
}
