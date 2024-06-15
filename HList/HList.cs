using System.Collections;

namespace HList
{
    public class HList<T> : IHList<T>, IEnumerable<T>
    {
        private readonly IList<T> _items;
        private readonly IDictionary<int, IList<int>> _valueIndexes;

        public HList()
        {
            _items = [];
            _valueIndexes = new Dictionary<int, IList<int>>();
        }

        public int Count => _items.Count;

        public void Add(T argument)
        {
            if(argument == null)
            {
                throw new ArgumentNullException();
            }

            var newIndex = _items.Count + 1;
            var hash = argument.GetHashCode();

            _items.Add(argument);

            if (_valueIndexes.TryGetValue(hash, out IList<int>? indexes))
            {
                indexes.Add(newIndex);
            } 
            else
            {
                _valueIndexes.Add(hash, [newIndex]);
            }
        }

        public IList<int>? Get(T argument)
        {
            if (argument == null)
            {
                throw new ArgumentNullException();
            }

            var hash = argument.GetHashCode();

            _valueIndexes.TryGetValue(hash, out IList<int>? indexes);

            return indexes;
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
