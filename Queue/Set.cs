using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue
{
    public class Set<T> : ISet<T>, ICloneable, ICollection<T>, IEnumerable, IEnumerable<T>, IReadOnlyCollection<T> where T:class
    {
        private T[] elements;
        private int counter;
        private int capacity = 16;
        private IEqualityComparer<T> equalityComparer;


        public int Count { get { return counter; } }
        public bool IsReadOnly { get { return false; } }
        public Set(IEqualityComparer<T> equalityComparer = null)
        {
            if (equalityComparer == null) this.equalityComparer = EqualityComparer<T>.Default;
            else this.equalityComparer = equalityComparer;

            elements = new T[capacity];
        }
        public Set(IEnumerable<T> args, IEqualityComparer<T> equalityComparer = null) : this(equalityComparer)
        {
            if(args==null)throw  new ArgumentNullException($"{nameof(args)} must not be null");

            foreach (T i in args)
            {
                Add(i);
            }
        }
        /// <summary>
        /// Adding elemets to set
        /// </summary>
        /// <param name="obj">elements to be added</param>
        /// <exception cref="ArgumentNullException">Argument must not be null</exception>
        /// <returns>true if seccess, false if not</returns>
        public bool Add(T obj)
        {
            if(obj==null) throw new ArgumentNullException($"{nameof(obj)} must not be null");

            if (Contains(obj)) return false;

            if (counter == capacity)
                Array.Resize(ref elements, capacity = 2 * capacity);

            elements[counter] = obj;
            counter++;
            return true;
        }
        void ICollection<T>.Add(T obj) { Add(obj); }
        /// <summary>
        /// Check if set contain this element
        /// </summary>
        /// <param name="obj">element you want to find</param>
        /// <exception cref="ArgumentNullException">Argument must not be null</exception>
        /// <returns>true if element is founded, false if not</returns>
        public bool Contains(T obj)
        {
            if (obj == null) throw new ArgumentNullException($"{nameof(obj)} must not be null");

            for (int i = 0; i < counter; i++)
            {
                if (equalityComparer.Equals(elements[i], obj)) return true;
            }
            return false;
        }
        /// <summary>
        /// Remove all elements from set
        /// </summary>
        public void Clear()
        {
            counter = 0;
            elements = new T[capacity];
        }

         void ICollection<T>.CopyTo(T[] newArray, int startingIndex = 0)
        {

            if (newArray == null) throw new ArgumentNullException();
            if (startingIndex < 0) throw new ArgumentOutOfRangeException();
            if (this.Count > newArray.Length) throw new ArgumentException();

            for (int i = 0; i < this.Count; i++)
                newArray[startingIndex + i] = elements[i];

        }
        /// <summary>
        /// Remove an element from the set
        /// </summary>
        /// <param name="obj">element to be removed</param>
        /// <exception cref="ArgumentNullException">Argument must not be null</exception>
        /// <returns>true if element removed seccessfully, false if not</returns>
        public bool Remove(T obj)
        {
            if (obj == null) throw new ArgumentNullException($"{nameof(obj)} must not be null");

            if (!Contains(obj)) return false;

            for (int i = 0; i < Count; i++)
            {
                if (equalityComparer.Equals(elements[i], obj))
                {
                    elements[i] = elements[Count - 1];
                    elements[Count - 1] = default(T);
                }
            }
            counter--;
            return true;
        }
        /// <summary>
        /// Removes all elements in the specified collection from the current set.
        /// </summary>
        /// <exception cref="ArgumentNullException">Argument must not be null</exception>
        /// <param name="other">The collection of items to remove from the set</param>
        public void ExceptWith(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException();

            foreach (T element in other)
            {
                if (Contains(element)) Remove(element);
            }
        }
        /// <summary>
        /// Modifies the current set so that it contains only elements that are also in a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <exception cref="ArgumentNullException">Argument must not be null</exception>
        public void IntersectWith(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException();

            SortedSet<T> newElements = new SortedSet<T>();

            foreach (T element in other)
            {
                if (Contains(element)) newElements.Add(element);
            }

            Clear();
            foreach (T element in newElements)
            {
                Add(element);
            }
        }
        /// <summary>
        /// Determines whether the current set is a proper (strict) subset of a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <exception cref="ArgumentNullException">Argument must not be null</exception>
        /// <returns></returns>
        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException();
           

            foreach (T i in this)
            {
                if (!other.Contains(i)) return false;
            }
            return true;
        }
        /// <summary>
        /// Determines whether the current set is a proper (strict) superset of a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <exception cref="ArgumentNullException">Argument must not be null</exception>
        /// <returns>true if the current set is a proper superset of other; otherwise, false.</returns>
        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException();

            foreach (T i in other)
            {
                if (!Contains(i)) return false;
            }
            return true;
        }
        /// <summary>
        /// Determines whether the current set is a proper (strict) subset of a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <exception cref="ArgumentNullException">Argument must not be null</exception>
        /// <returns></returns>
        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return IsProperSubsetOf(other);
        }
        /// <summary>
        /// Determines whether the current set is a proper (strict) superset of a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <exception cref="ArgumentNullException">Argument must not be null</exception>
        /// <returns>true if the current set is a proper superset of other; otherwise, false.</returns>
        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return IsProperSupersetOf(other);
        }
        /// <summary>
        /// Determines whether the current set overlaps with the specified collection
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <exception cref="ArgumentNullException">Argument must not be null</exception>
        /// <returns>true if the current set and other share at least one common element; otherwise, false.</returns>
        public bool Overlaps(IEnumerable<T> other)                    
        {
            if (other == null) throw new ArgumentNullException();
            return new SortedSet<T>(other).Count.Equals(this.Count);
        }
        /// <summary>
        /// Determines whether the current set and the specified collection contain the same elements.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <exception cref="ArgumentNullException">Argument must not be null</exception>
        /// <returns></returns>
        public bool SetEquals(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException();
            return IsProperSubsetOf(other) && IsProperSupersetOf(other);
        }
        /// <summary>
        /// Modifies the current set so that it contains only elements
        ///  that are present either in the current set or in the specified collection, but not both.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <exception cref="ArgumentNullException">Argument must not be null</exception>
        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException();

            foreach (T element in other)
            {
                if (!Contains(element)) Add(element);
            }
        }
        /// <summary>
        /// Modifies the current set so that it contains all
        ///  elements that are present in the current set, in the specified collection, or in both.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <exception cref="ArgumentNullException">Argument must not be null</exception>
        public void UnionWith(IEnumerable<T> other)
        {
            this.SymmetricExceptWith(other);
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
        /// <summary>
        /// Make a clone set of this one
        /// </summary>
        /// <returns>new Set, wich is copy of this one</returns>
        public Set<T> Clone()
        {
            Set<T> newRes = new Set<T>();
            foreach (T i in this)
            {
                newRes.Add(i);
            }
            return newRes;
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        /// <summary>
        /// iterator
        /// </summary>
        /// <returns>IEnumerable object</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return elements[i];
        }

    }
}
