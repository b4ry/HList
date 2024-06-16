using System.Collections;

namespace HList
{
    public class HList<T> : IHList<T>, IEnumerable<T>
    {
        private readonly IList<T> _items;
        private readonly IDictionary<int, HashSet<int>> _valueIndexes;

        public HList()
        {
            _items = [];
            _valueIndexes = new Dictionary<int, HashSet<int>>();
        }

        public int Count => _items.Count;

        public void Add(T argument)
        {
            if(argument == null)
            {
                throw new ArgumentNullException();
            }

            var newIndex = _items.Count;
            var hash = argument.GetHashCode();

            _items.Add(argument);

            if (_valueIndexes.TryGetValue(hash, out HashSet<int>? indexes))
            {
                indexes.Add(newIndex);
            } 
            else
            {
                _valueIndexes.Add(hash, [newIndex]);
            }
        }

        public HashSet<int>? Get(T argument)
        {
            if (argument == null)
            {
                throw new ArgumentNullException();
            }

            var hash = argument.GetHashCode();

            _valueIndexes.TryGetValue(hash, out HashSet<int>? indexes);

            return indexes;
        }

        public void Remove(T argument)
        {
            if(argument == null)
            {
                throw new ArgumentNullException();
            }

            var indexes = Get(argument)!.OrderByDescending(x => x);

            _valueIndexes.Remove(argument!.GetHashCode());

            foreach(var index in indexes)
            {
                _items.RemoveAt(index);
            }
        }

        public T this[int index]
        {
            get
            {
                return _items[index];
            }

            set
            {
                _items[index] = value;
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
    }
}
