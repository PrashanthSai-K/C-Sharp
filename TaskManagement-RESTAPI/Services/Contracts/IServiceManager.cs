using System;

namespace TaskManagement_RESTAPI.Services.Contracts;

public interface IServiceManager
{
    public IUserService UserService {get;}

    public ITaskItemService     TaskItemService {get;}


}
