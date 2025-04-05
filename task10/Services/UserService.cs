using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using task10.AppDataContext;
using task10.Contarcts;
using task10.Interface;
using task10.Models;

namespace task10.Services;

public class UserService : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<UserService> _logger;
    private readonly IMapper _mapper;

    public UserService(ApplicationDbContext context, ILogger<UserService> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task CreateUser(CreateUser user)
    {
        var User = _mapper.Map<User>(user);
        await _context.Users.AddAsync(User);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByName(string name)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == name);
            if (user == null)
                return null;
            return user;
        }
        catch (System.Exception ex)
        {
            _logger.LogError($"{ex.Message} Error");
            throw;
        }
    }
}
