using System;
using AutoMapper;
using TaskManagement_RESTAPI.Entities.Models;
using TaskManagement_RESTAPI.Entities.RequestParams;
using TaskManagement_RESTAPI.Exceptions;
using TaskManagement_RESTAPI.Repositories.Interfaces;
using TaskManagement_RESTAPI.Services.Contracts;
using TaskManagement_RESTAPI.Shared.DTO;

namespace TaskManagement_RESTAPI.Services.Concrete;

public class TaskItemService : ITaskItemService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILogger<TaskItemService> _logger;
    private readonly IMapper _mapper;

    public TaskItemService(IRepositoryManager repositoryManager, ILogger<TaskItemService> logger, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaskItem>> GetAllTasks(TaskQueryParams queryParams)
    {
        return await _repositoryManager.TaskItem.GetAllTasks(queryParams);
    }

    public async Task<TaskItem?> GetTaskById(int id)
    {
        return await _repositoryManager.TaskItem.GetTaskById(id) ?? throw new TaskNotFoundException(id);
    }

    public async Task CreateTask(CreateTask item)
    {
        var TaskItem = _mapper.Map<TaskItem>(item);
        var user = await _repositoryManager.User.GetUserById(TaskItem.UserId) ?? throw new UserNotFoundException(TaskItem.UserId);
        _repositoryManager.TaskItem.CreateTask(TaskItem);
        await _repositoryManager.SaveAsync();
    }

    public async Task UpdateTask(UpdateTask item)
    {
        var TaskItem = _mapper.Map<TaskItem>(item);
        var task = await _repositoryManager.TaskItem.GetTaskById(TaskItem.Id) ?? throw new TaskNotFoundException(TaskItem.Id);
        TaskItem.UserId = task.UserId;
        _repositoryManager.TaskItem.UpdateTask(TaskItem);
        await _repositoryManager.SaveAsync();
    }

    public async Task DeleteTask(int id)
    {
        var task = await _repositoryManager.TaskItem.GetTaskById(id) ?? throw new TaskNotFoundException(id);
        _repositoryManager.TaskItem.DeleteTask(task);
        await _repositoryManager.SaveAsync();
    }

}
