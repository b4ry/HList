namespace HList
{
    public interface IHList<T>
    {
        public void Add(T argument);
        public HashSet<int>? Get(T argument);
        public void Remove(T argument);
    }
}
