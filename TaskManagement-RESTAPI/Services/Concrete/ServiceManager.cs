using System;
using TaskManagement_RESTAPI.Services.Contracts;

namespace TaskManagement_RESTAPI.Services.Concrete;

public class ServiceManager : IServiceManager
{
    private readonly ITaskItemService _taskItemService;
    private readonly IUserService _userService;
    public ServiceManager(ITaskItemService taskItemService, IUserService userService)
    {
        _taskItemService = taskItemService;
        _userService = userService;
    }
    public IUserService UserService => _userService;

    public ITaskItemService TaskItemService => _taskItemService;

}
