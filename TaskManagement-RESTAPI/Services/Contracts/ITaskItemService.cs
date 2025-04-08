using System;
using TaskManagement_RESTAPI.Entities.Models;
using TaskManagement_RESTAPI.Shared.DTO;

namespace TaskManagement_RESTAPI.Services.Contracts;

public interface ITaskItemService
{
    Task<IEnumerable<TaskItem>> GetAllTasks();

    Task<TaskItem?> GetTaskById(int id);

    Task CreateTask(CreateTask item);
    Task UpdateTask(UpdateTask item);
    Task DeleteTask(int id);
}
