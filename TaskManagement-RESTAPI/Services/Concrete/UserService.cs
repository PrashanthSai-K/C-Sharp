using System;
using TaskManagement_RESTAPI.Entities.Models;
using TaskManagement_RESTAPI.Repositories.Interfaces;
using TaskManagement_RESTAPI.Services.Contracts;

namespace TaskManagement_RESTAPI.Services.Concrete;

public class UserService : IUserService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILogger<UserService> _logger;

    public UserService(IRepositoryManager repositoryManager, ILogger<UserService> logger)
    {
        _repositoryManager = repositoryManager;
        _logger = logger;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _repositoryManager.User.GetAllUsers();
    }

    public async Task<User?> GetUserById(int id)
    {
        return await _repositoryManager.User.GetUserById(id);
    }
}
