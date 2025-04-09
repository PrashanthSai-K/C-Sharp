using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagement_RESTAPI.AppDataContext;
using TaskManagement_RESTAPI.Entities.Models;
using TaskManagement_RESTAPI.Entities.RequestParams;
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

    public async Task<IEnumerable<TaskItem>> GetAllTasks(TaskQueryParams queryParams)
    {
        var tasks = GetAll();

        if (queryParams.IsCompleted.HasValue)
            tasks = tasks.Where(t => t.IsCompleted == queryParams.IsCompleted);

        if (queryParams.UserId.HasValue)
            tasks = tasks.Where(t => t.UserId == queryParams.UserId);

        if (queryParams.IsCompleted.HasValue)
            tasks = tasks.Where(t => t.IsCompleted == queryParams.IsCompleted);

        tasks = queryParams.SortBy?.ToLower() switch
        {
            "name" => queryParams.Descending ? tasks.OrderByDescending(t => t.Name) : tasks.OrderBy(t => t.Name),
            "duedate" => queryParams.Descending ? tasks.OrderByDescending(t => t.DueDate) : tasks.OrderBy(t => t.DueDate),
            _ => tasks.OrderBy(t => t.DueDate)
        };

        return await tasks.Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
                        .Take(queryParams.PageSize)
                        .ToListAsync();
    }

    public async Task<TaskItem?> GetTaskById(int id)
    {
        var task = await GetByCondition(item => item.Id == id)
                            .AsNoTracking()
                            .FirstOrDefaultAsync();
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
