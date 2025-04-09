using System;
using TaskManagement_RESTAPI.Entities.Models;
using TaskManagement_RESTAPI.Shared.DTO;

namespace TaskManagement_RESTAPI.Services.Contracts;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsers();

    Task<User?> GetUserById(int id);

    Task CreateUser(CreateUser user);
    Task UpdateUser(UpdateUser user);
    Task DeleteUser(int id);
    Task<User> AuthenticateUser(LoginDTO login);
}
