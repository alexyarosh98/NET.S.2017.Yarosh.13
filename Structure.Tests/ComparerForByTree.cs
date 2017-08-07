using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structure.Tests
{
    class ComparerForByTree:IComparer<int>
    {
        public int Compare(int obj1, int obj2)
            => Math.Abs(obj1 - obj2);
    }

}
