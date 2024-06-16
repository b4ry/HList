using System.Collections;

namespace HList
{
    public class HList<T> : IHList<T>, IEnumerable<T>
    {
        private readonly IList<T> _items;
        private readonly Dictionary<int, HashSet<int>> _valueIndexes;

        public HList()
        {
            _items = [];
            _valueIndexes = [];
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
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                var previousValue = _items[index];

                AddAtIndex(value, index);

                var previousValueHashCode = previousValue!.GetHashCode();
                var previousValueIndexes = _valueIndexes[previousValueHashCode];

                previousValueIndexes.Remove(index);

                if (previousValueIndexes.Count == 0)
                {
                    _valueIndexes.Remove(previousValueHashCode);
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

        private void AddAtIndex(T argument, int index)
        {
            if (argument == null)
            {
                throw new ArgumentNullException();
            }

            var hash = argument.GetHashCode();

            _items[index] = argument;

            if (_valueIndexes.TryGetValue(hash, out HashSet<int>? indexes))
            {
                indexes.Add(index);
            }
            else
            {
                _valueIndexes.Add(hash, [index]);
            }
        }
    }
}
