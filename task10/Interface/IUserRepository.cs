using System;
using task10.Contarcts;
using task10.Models;

namespace task10.Interface;

public interface IUserRepository
{
    Task<User?> GetByName(string name);
    Task CreateUser(CreateUser user);
}
