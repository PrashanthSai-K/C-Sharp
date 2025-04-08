using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskManagement_RESTAPI.AppDataContext;
using TaskManagement_RESTAPI.Repositories.Interfaces;

namespace TaskManagement_RESTAPI.Repositories.Base;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected ApplicationDbContext context;

    public GenericRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public IQueryable<T> GetAll()
    {
        return context.Set<T>();
    }

    public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
    {
        return context.Set<T>().Where(expression);
    }

    public async Task<T?> GetById(int id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public async void Create(T item)
    {
        await context.Set<T>().AddAsync(item);
    }

    public void Delete(T item)
    {
        context.Set<T>().Remove(item);
    }

    public void Update(T item)
    {
        context.Set<T>().Update(item);
    }
}
