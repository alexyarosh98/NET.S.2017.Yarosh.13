using System;
using System.Collections;
using System.Collections.Generic;

namespace Structure
{
    public class Queue<T> : IEnumerable<T>,ICloneable
    {
        private T[] array;
        private int head;
        private int tail;
        private int counter;
        private int capacity = 8;
        public int Count { get { return counter; } }
        public bool IsReadOnly { get { return false; } }

        public Queue()
        {
            array = new T[capacity];
        }
        public Queue(IEnumerable<T> obj) : this()
        {
            if (obj == null) throw new ArgumentNullException();
            foreach (var i in obj)
            {
                Enqueue(i);
            }
        }

        public void Enqueue(T obj)
        {
            if (obj == null) throw new ArgumentNullException();
            if (counter == array.Length)
            {
                Resize(counter*2);
            }
            array[tail] = obj;
            tail = (tail+1) % array.Length;
            counter++;
         }

        public T Dequeue()
        {
            if (Count == 0) throw new InvalidOperationException("No elements in the queue");
            T removedObj=array[head];
            array[head] = default(T);
            head = (head + 1) % array.Length;
            counter--;
            return removedObj;
         }
        public void Clear()
        {
            array = new T[capacity];
            head = 0;
            tail = 0;
            counter = 0;
        }
        public bool Contains(T obj, EqualityComparer<T> comparer = null)
        {
            if (obj == null) return false;
            if(comparer==null)comparer = EqualityComparer<T>.Default;
            int size = Count;
            int index = head;
            while (size-- > 0)
            {
                if (index == array.Length) index = 0;
                if (comparer.Equals(array[index], obj)) return true;
                index++;
            }
            return false;
        }
        public T Peek()
        {
            if(Count==0) throw new InvalidOperationException("No elements in the queue");
            return array[head];
        }
        private void Resize(int newSize)
        {
            T[] newArray=new T[newSize];
            if (head < tail)
            {
                Array.Copy(array, head, newArray, 0, counter);
            }
            else
            {
                Array.Copy(array, head, newArray, 0, array.Length - head);
                Array.Copy(array, 0, newArray, array.Length - head, tail);
            }
            array = newArray;
            head = 0;
            tail = counter;
        }
        public Object Clone()
        {
            Queue<T> cloned = new Queue<T>();
            foreach (T i in array)
            {
                cloned.Enqueue(i);
            }
            return cloned;
        }
        public void Trim()
        {
            Resize(counter);
        }
        public IEnumerator<T> GetEnumerator()
        {
            int index = head;
            int size = Count;
            while (size-- > 0)
            {
                if (index == array.Length) index = 0;
                yield return array[index];
                index++;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return array.GetEnumerator();
        }
    }
}
