using System;

namespace TaskManagement_RESTAPI.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(int id) :  base($"User with Id : {id} not found")
    {
    }
}
