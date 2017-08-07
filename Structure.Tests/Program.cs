using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using Queue;

namespace Structure.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Queue of integers
            
            Structure.Queue<int> intQueue=new Queue<int>(new int[]{1,2,3,4,5,2});
            Show(intQueue);

            Console.WriteLine("Lets add some elements to the queue");
            intQueue.Enqueue(10);
            intQueue.Enqueue(-41);
            Show(intQueue);

            Console.WriteLine("Lets remove 3 elements from the queue");
            intQueue.Dequeue();
            intQueue.Dequeue();
            intQueue.Dequeue();
            Show(intQueue);

            Console.WriteLine("First element in the queue is : "+intQueue.Peek());

            Console.WriteLine("Lets delete the queue");
            intQueue.Clear();
            Show(intQueue);

            #endregion

            #region Queue of strings

            Console.WriteLine("\n\nNew Queue of strings: ");
            Queue<string> stringQueue=new Queue<string>(new[]{"first","second","third","fourth","fifth"});
            Show(stringQueue);

            Console.WriteLine("Lets add some elements to the queue");
            stringQueue.Enqueue("sixth");
            stringQueue.Enqueue("seventh");
            Show(stringQueue);

            Console.WriteLine("Lets remove 3 elements from the queue");
            stringQueue.Dequeue();
            stringQueue.Dequeue();
            stringQueue.Dequeue();
            Show(stringQueue);

            Console.WriteLine($"Queue contains sixth : {stringQueue.Contains("sixth")}");
            Console.WriteLine($"Queue contains Sixth with defaultcomparer : {stringQueue.Contains("Sixth")}");
            Console.WriteLine($"Queue contains Sixth with nonedafaultcomparer : {stringQueue.Contains("Sixth",new NoneDefaultStringComparer())}");

            Console.WriteLine("\nFirst element in the queue is : " + stringQueue.Peek());

            Console.WriteLine("Lets delete the queue");
            stringQueue.Clear();
            Show(stringQueue);

            #endregion

            #region Set of integers
            
            Set<string> intSet = new Set<string>(new[] { "1","4", "4", "6", "3", "10", "3" });
            Show(intSet);

            Console.WriteLine("Lets add some elements to the set");
            intSet.Add("18");
            intSet.Add("22");
            intSet.Add("1");
            Show(intSet);

            Console.WriteLine("Lets delete some elements from the set");
            intSet.Remove("4");
            intSet.Remove("10");
            Show(intSet);

            Console.WriteLine("Lets copy our set to another one");
            Set<string> newIntSet=intSet.Clone();
            Show(newIntSet);
            Console.WriteLine($"ReferenceEquqls: {ReferenceEquals(intSet,newIntSet)}");

            Console.WriteLine($"Are this sets equals ? {intSet.SetEquals(newIntSet)}");

            Console.WriteLine("\nLets add some elements to old set");
            intSet.Add("100");

            Console.WriteLine($"\nIs an old set is a superset of new set? {intSet.IsSupersetOf(newIntSet)}");
            Console.WriteLine($"\nIs a new set is a subset of old one? {newIntSet.IsSubsetOf(intSet)}");
            Console.WriteLine($"\nAre these 2 set overlapsed? {newIntSet.Overlaps(intSet)}");

            Console.WriteLine("\nLets use Symmetric exeptwith and try again");
            newIntSet.SymmetricExceptWith(intSet);
            Console.WriteLine($"Are these 2 set overlapsed? {newIntSet.Overlaps(intSet)}");

            intSet.Add("222");
            intSet.Add("333");
            Console.WriteLine("222 and 333 were added to an old set. And now lets Intersect it with new one");
            intSet.IntersectWith(newIntSet);
            Show(intSet);

            Console.WriteLine("Lets look at new set exceptwith old one");
            newIntSet.ExceptWith(intSet);
            Show(newIntSet);

            #endregion

            #region BiTree Int32 with default and not default comparers
            Console.WriteLine("Lets create new binary tree<int> with default comparer");
            BiTree<int> biTreeInt=new BiTree<int>(new int[]{10,15,-7,7,14,3,2,1,22});
            Show(biTreeInt);
            Console.WriteLine("Tree contains 22: "+biTreeInt.Contains(22)+"\n Tree contains -16:"+biTreeInt.Contains(-16));

            Console.WriteLine("Lets remove 3");
            biTreeInt.Remove(3);
            Show(biTreeInt);

            Console.WriteLine("Lets create new binary trr<int> with logic of compearing int by Math.Abs");
            BiTree<int> biTreeInt2 = new BiTree<int>(new int[] { 10, 15, -7, 7, 14, 3, 2, 1, 22 },NoneDefaultComparisionForIntTree);
            Show(biTreeInt2);
            Console.WriteLine("Tree contains 22: " + biTreeInt2.Contains(22) + "\n Tree contains -16:" + biTreeInt2.Contains(-16));

            Console.WriteLine("Lets remove 3");
            biTreeInt2.Remove(3);
            Show(biTreeInt2);

            Console.WriteLine("Lets create new trii but with interface parametr");
            BiTree<int> newByTree=new BiTree<int>(new []{-123,32,312,31,5,12,13,515},new ComparerForByTree());
            Show(newByTree);

            #endregion

            #region BiTree String with default and nonedefault comparers

            Console.WriteLine("Lets create a tree of string with default comparer");
            BiTree<string> biTreeStrings=new BiTree<string>(new []{"onde","two","three","four","five","six","seven"});
            Show(biTreeStrings);

            Console.WriteLine("Tree contains onde: "+biTreeStrings.Contains("onde")+"\nTree contains nine"+biTreeStrings.Contains("nine"));

            Console.WriteLine("Lets remove four");
            biTreeStrings.Remove("four");
            Show(biTreeStrings);


            Console.WriteLine("Lets create a tree of string with comparer of lenth of string");
            BiTree<string> biTreeStrings2 = new BiTree<string>(new[] { "onde", "two", "three", "four", "five", "six", "seven" });
            Show(biTreeStrings2);

            Console.WriteLine("Tree contains onde: " + biTreeStrings2.Contains("onde") + "\nTree contains nine" + biTreeStrings2.Contains("nine"));

            Console.WriteLine("Lets remove four");
            biTreeStrings.Remove("four");
            Show(biTreeStrings2);


            #endregion>
            Console.ReadKey();
        }
        #region Show functions
        public static void Show<T>(Queue<T> queue)
        {
            Console.WriteLine("Queue now consist if "+ queue.Count+" elements: ");
            foreach (T element in queue)
            {
                Console.Write(element + " ");
            }
            Console.WriteLine("\n");
        }
        public static void Show<T>(Set<T> queue) where T :class 
        {
            Console.WriteLine("Set now consist of " + queue.Count + " elements: ");
            foreach (T element in queue)
            {
                Console.Write(element + " ");
            }
            Console.WriteLine("\n");
        }

        public static void Show<T>(BiTree<T> biTree)
        {
            Console.WriteLine("Tree now consist of " + biTree.Count + " elements(inorder version):");
            foreach (var element in biTree.Inorder())
            {
                Console.Write(element+" ");
            }
            Console.WriteLine("\n");
            Console.WriteLine("Preoder version of the same tree");
            foreach (var element in biTree.Preorder())
            {
                Console.Write(element+" ");
            }
            Console.WriteLine("\n");
        }
        #endregion
        public static int NoneDefaultComparisionForIntTree(int obj1, int obj2)
            => Math.Abs(obj1).CompareTo(Math.Abs(obj2));

        public static int NoneDefaultComparisionForStringTree(string obj1, string obj2)
            => obj1.Length.CompareTo(obj2.Length);
        public class NoneDefaultStringComparer : IEqualityComparer<string>
        {
            public bool Equals(string str1, string str2)
            {
                return str1.Equals(str2, StringComparison.InvariantCultureIgnoreCase);
            }

            public int GetHashCode(string a)
            {
                return a.GetHashCode();
            }
        }
    }
    
}
