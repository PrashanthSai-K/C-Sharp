using System;
using TaskManagement_RESTAPI.Entities.Models;

namespace TaskManagement_RESTAPI.Repositories.Interfaces;

public interface ITaskItemRepository
{
    Task<IEnumerable<TaskItem>> GetAllTasks();
    Task<TaskItem> GetTaskById(int id);
    void CreateTask(TaskItem item);
    void UpdateTask(TaskItem item);
    void DeleteTask(TaskItem item);
}
