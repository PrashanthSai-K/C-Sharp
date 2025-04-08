using System;
using System.Threading.Tasks;
using TaskManagement_RESTAPI.AppDataContext;
using TaskManagement_RESTAPI.Repositories.Interfaces;

namespace TaskManagement_RESTAPI.Repositories.Concrete;

public class RepositoryManager : IRepositoryManager
{
    private readonly ApplicationDbContext _context;
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IUserRepository _userRepositoty;

    public RepositoryManager(ApplicationDbContext context, ITaskItemRepository taskItemRepository, IUserRepository userRepository)
    {
        _context = context;
        _taskItemRepository = taskItemRepository;
        _userRepositoty = userRepository;
    }

    public ITaskItemRepository TaskItem => _taskItemRepository;
    public IUserRepository User => _userRepositoty;

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
