using System;
using TaskManagement_RESTAPI.Entities.Models;
using TaskManagement_RESTAPI.Entities.RequestParams;

namespace TaskManagement_RESTAPI.Repositories.Interfaces;

public interface ITaskItemRepository
{
    Task<IEnumerable<TaskItem>> GetAllTasks(TaskQueryParams queryParams);
    Task<TaskItem?> GetTaskById(int id);
    void CreateTask(TaskItem item);
    void UpdateTask(TaskItem item);
    void DeleteTask(TaskItem item);
}
