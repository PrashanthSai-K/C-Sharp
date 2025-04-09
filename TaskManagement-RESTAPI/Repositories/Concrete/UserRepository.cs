using System;
using Microsoft.EntityFrameworkCore;
using TaskManagement_RESTAPI.AppDataContext;
using TaskManagement_RESTAPI.Entities.Models;
using TaskManagement_RESTAPI.Exceptions;
using TaskManagement_RESTAPI.Repositories.Base;
using TaskManagement_RESTAPI.Repositories.Interfaces;

namespace TaskManagement_RESTAPI.Repositories.Concrete;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await GetAll().ToListAsync();
    }

    public async Task<User?> GetUserById(int id)
    {
        var user = await GetByCondition(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        return user;
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        var user = await GetByCondition(u => u.Username == username).AsNoTracking().FirstOrDefaultAsync();
        return user;
    }
    public async Task<User?> GetUserByEmail(string email)
    {
        var user = await GetByCondition(u => u.Email == email).AsNoTracking().FirstOrDefaultAsync();
        return user;
    }

    public void CreateUser(User user)
    {
        Create(user);
    }

    public void UpdateUser(User user)
    {
        Update(user);
    }

    public void DeleteUser(User user)
    {
        Delete(user);
    }

}
