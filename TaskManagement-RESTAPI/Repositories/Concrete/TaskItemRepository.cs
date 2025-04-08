using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagement_RESTAPI.AppDataContext;
using TaskManagement_RESTAPI.Entities.Models;
using TaskManagement_RESTAPI.Exceptions;
using TaskManagement_RESTAPI.Repositories.Base;
using TaskManagement_RESTAPI.Repositories.Interfaces;

namespace TaskManagement_RESTAPI.Repositories.Concrete;

public class TaskItemRepository : GenericRepository<TaskItem>, ITaskItemRepository
{
    private readonly ApplicationDbContext _context;

    public TaskItemRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskItem>> GetAllTasks()
    {
        return await GetAll().ToListAsync();
    }

    public async Task<TaskItem> GetTaskById(int id)
    {
        var task = await GetByCondition(item => item.Id == id)
                            .AsNoTracking()
                            .FirstOrDefaultAsync() 
                            ?? throw new TaskNotFoundException(id);
        return task;
    }

    public void CreateTask(TaskItem task)
    {
        Create(task);
    }

    public void UpdateTask(TaskItem item)
    {
        Update(item);
    }

    public void DeleteTask(TaskItem item)
    {
        Delete(item);
    }
}
