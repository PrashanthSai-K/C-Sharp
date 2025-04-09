using System;
using AutoMapper;
using TaskManagement_RESTAPI.Entities.Models;
using TaskManagement_RESTAPI.Exceptions;
using TaskManagement_RESTAPI.Repositories.Interfaces;
using TaskManagement_RESTAPI.Services.Contracts;
using TaskManagement_RESTAPI.Shared.DTO;

namespace TaskManagement_RESTAPI.Services.Concrete;

public class UserService : IUserService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILogger<UserService> _logger;
    private readonly IMapper _mapper;

    public UserService(IRepositoryManager repositoryManager, ILogger<UserService> logger, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _repositoryManager.User.GetAllUsers();
    }

    public async Task<User?> GetUserById(int id)
    {
        return await _repositoryManager.User.GetUserById(id) ?? throw new UserNotFoundException(id); ;
    }

    public async Task CreateUser(CreateUser user)
    {
        var User = _mapper.Map<User>(user);
        var userFound = await _repositoryManager.User.GetUserByUsername(user.Username ?? string.Empty);
        if (userFound != null)
            throw new ConflictException("Username already taken");
        userFound = await _repositoryManager.User.GetUserByEmail(user.Email ?? string.Empty);
        if (userFound != null)
            throw new ConflictException("Email already exists");
        _repositoryManager.User.CreateUser(User);
        await _repositoryManager.SaveAsync();
    }

    public async Task UpdateUser(UpdateUser user)
    {
        var User = _mapper.Map<User>(user);
        var OldUser = await _repositoryManager.User.GetUserById(User.Id) ?? throw new UserNotFoundException(user.Id);
        var userFound = await _repositoryManager.User.GetUserByUsername(user.Username ?? string.Empty);
        if (userFound != null)
            throw new ConflictException("Username already taken");
        userFound = await _repositoryManager.User.GetUserByEmail(user.Email ?? string.Empty);
        if (userFound != null)
            throw new ConflictException("Email already exists");
        _repositoryManager.User.UpdateUser(User);
        await _repositoryManager.SaveAsync();
    }

    public async Task DeleteUser(int id)
    {
        var user = await _repositoryManager.User.GetUserById(id) ?? throw new UserNotFoundException(id); ;
        _repositoryManager.User.DeleteUser(user);
        await _repositoryManager.SaveAsync();
    }

    public async Task<User> AuthenticateUser(LoginDTO login)
    {
        var user = await _repositoryManager.User.GetUserByUsername(login.UserName) ?? throw new UsernameNotFoundException(login.UserName);
        if (!(user?.Password?.Equals(login.Password) ?? false))
        {
            throw new LoginPasswordWrongException();
        }
        return user;
    }
}
