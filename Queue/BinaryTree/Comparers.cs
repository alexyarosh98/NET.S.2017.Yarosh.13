using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structure.BinaryTree
{
    public static class Comparers
    {
        public static int DefaultComparision<T>(T obj1, T obj2)
            => obj1.GetHashCode().CompareTo(obj2.GetHashCode());

    }
}
