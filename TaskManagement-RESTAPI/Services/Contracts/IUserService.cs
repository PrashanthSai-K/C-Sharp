using System;
using TaskManagement_RESTAPI.Entities.Models;

namespace TaskManagement_RESTAPI.Services.Contracts;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsers();

    Task<User?> GetUserById(int id);

}
