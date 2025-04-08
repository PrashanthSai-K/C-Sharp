using System;
using System.Linq.Expressions;
using TaskManagement_RESTAPI.Entities.Models;

namespace TaskManagement_RESTAPI.Repositories.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsers();
    // IQueryable<User> GetByCondition(Expression<Func<User, bool>> expression);
    Task<User?> GetUserById(int id);

}
