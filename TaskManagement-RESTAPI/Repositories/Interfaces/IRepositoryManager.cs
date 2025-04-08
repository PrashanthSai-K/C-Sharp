using System;

namespace TaskManagement_RESTAPI.Repositories.Interfaces;

public interface IRepositoryManager
{
    ITaskItemRepository TaskItem {get;}
    IUserRepository User {get;}
    Task SaveAsync();
}
