using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structure.BinaryTree
{
    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
        /// <summary>
        /// Create node of the tree with element inside
        /// </summary>
        /// <param name="obj">element inside the node</param>
        /// <exception cref="ArgumentNullException">argument must not be null</exception>
        public Node(T obj)
        {
            if (ReferenceEquals(obj, null)) throw new ArgumentNullException($"{nameof(obj)} must not be null");

            Value = obj;
        }

    }
}
