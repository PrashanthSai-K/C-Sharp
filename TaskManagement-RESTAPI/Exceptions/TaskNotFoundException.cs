using System;

namespace TaskManagement_RESTAPI.Exceptions;

public class TaskNotFoundException : Exception
{
    public TaskNotFoundException(int id) : base ($"Task with Id : {id} not found")
    {

    }
}
