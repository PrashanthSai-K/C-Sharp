using System;

namespace task8;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly List<T> _items = new List<T>();

    public List<T> GetAll()
    {
        return _items;
    }

    public T? Get(int id)
    {
        return _items.ElementAtOrDefault(id);
    }

    public void Add(T item)
    {
        _items.Add(item);
    }

    public void Update(int id, T item)
    {
        if (id >= 0 && id <= _items.Count)
            _items[id] = item;
    }

    public void Delete(int id)
    {
        if (id >= 0 && id <= _items.Count)
            _items.RemoveAt(id);
    }

}
