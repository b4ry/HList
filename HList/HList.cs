using System.Collections;

namespace HList
{
    /** A hash list data structure:
     *  - value retrieval via an index works the same was as it does for array lists,
     *  - value indexes retrieval depends on an object's GetHashCode method implementation, but usually in O(1) time,
     *  - value addition in amortzied O(1) time; sometimes it is more, when needs to enlarge the underlying list
     *  - value removal in O(n^2) time; removes all value occurences,
     *  - preserves the list's order. No list reordering, when searching for a value,
     *  - space complexity is in O(3n) => O(n).
     *  
     *  Since hashing is utilized, to make the list work properly with custom objects,
     *  it is recommended to override their GetHashCode methods, eg.
     *  
     *  public class TestObject
        {
            public int TestIntProperty { get; set; }
            public string TestStringProperty { get; set; }

            public override int GetHashCode()
            {
                return TestIntProperty.GetHashCode() ^ TestStringProperty.GetHashCode();
            }
        }
     */
    public class HList<T> : IHList<T>, IEnumerable<T>
    {
        private readonly IList<T> _items; // space: O(n)
        private readonly Dictionary<int, HashSet<int>> _valueIndexes; // space: O(2n) => O(n)

        public HList()
        {
            _items = [];
            _valueIndexes = [];
        }

        public int Count => _items.Count;

        public void Add(T argument) // depends on the GetHashCode implementation, but usually amortized O(1)
        {
            if(argument == null)
            {
                throw new ArgumentNullException();
            }

            var newIndex = _items.Count;
            var hash = argument.GetHashCode(); // depends on the underlying implementation

            _items.Add(argument); // amortized O(1)

            if (_valueIndexes.TryGetValue(hash, out HashSet<int>? indexes)) // O(1)
            {
                indexes.Add(newIndex); // amortized O(1)
            } 
            else
            {
                _valueIndexes.Add(hash, [newIndex]); // amortized O(1)
            }
        }

        public HashSet<int>? GetIndexes(T argument) // depends on the GetHashCode implementation, but usually O(1)
        {
            if (argument == null)
            {
                throw new ArgumentNullException();
            }

            var hash = argument.GetHashCode(); // depends on the underlying implementation

            _valueIndexes.TryGetValue(hash, out HashSet<int>? indexes); // O(1)

            return indexes;
        }

        public void Remove(T argument) // O(n^2)
        {
            if(argument == null)
            {
                throw new ArgumentNullException();
            }

            var indexes = GetIndexes(argument)!.OrderByDescending(x => x); // O(nlogn)

            _valueIndexes.Remove(argument!.GetHashCode()); // O(1)

            foreach(var index in indexes) // O(n)
            {
                _items.RemoveAt(index); // O(n)
            }
        }

        public T this[int index]
        {
            get
            {
                return _items[index]; // O(1)
            }

            set // depends on the GetHashCode implementation, but usually O(1)
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                var previousValue = _items[index]; // O(1)

                AddAtIndex(value, index); // depends on the underlying implementation, but usually amortized O(1)

                var previousValueHashCode = previousValue!.GetHashCode(); // depends on the underlying implementation
                var previousValueIndexes = _valueIndexes[previousValueHashCode]; // O(1)

                previousValueIndexes.Remove(index); // O(1)

                if (previousValueIndexes.Count == 0)
                {
                    _valueIndexes.Remove(previousValueHashCode); // O(1)
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void AddAtIndex(T argument, int index) // depends on the GetHashCode implementation, but usually O(1)
        {
            if (argument == null)
            {
                throw new ArgumentNullException();
            }

            var hash = argument.GetHashCode(); // depends on the underlying implementation, but usually O(1)

            _items[index] = argument; // O(1)

            if (_valueIndexes.TryGetValue(hash, out HashSet<int>? indexes)) // O(1)
            {
                indexes.Add(index); // amortized O(1)
            }
            else
            {
                _valueIndexes.Add(hash, [index]); // amortized O(1)
            }
        }
    }
}
