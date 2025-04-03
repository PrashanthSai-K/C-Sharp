using System;

namespace task8;

public interface IRepository<T>
{
    List<T> GetAll();
    T? Get(int id);
    void Add(T item);
    void Update(int id, T item);
    void Delete(int id);
}
