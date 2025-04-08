using System;
using System.Linq.Expressions;

namespace TaskManagement_RESTAPI.Repositories.Interfaces;

public interface IGenericRepository<T>
{
    IQueryable<T> GetAll();
    IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);
    Task<T?> GetById(int id);
    void Create(T item);
    void Update(T item);
    void Delete(T item);
}
