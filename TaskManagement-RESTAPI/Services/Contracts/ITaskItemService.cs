using System;
using TaskManagement_RESTAPI.Entities.Models;
using TaskManagement_RESTAPI.Entities.RequestParams;
using TaskManagement_RESTAPI.Shared.DTO;

namespace TaskManagement_RESTAPI.Services.Contracts;

public interface ITaskItemService
{
    Task<IEnumerable<TaskItem>> GetAllTasks(TaskQueryParams queryParams);

    Task<TaskItem?> GetTaskById(int id);

    Task CreateTask(CreateTask item);
    Task UpdateTask(UpdateTask item);
    Task DeleteTask(int id);
}
