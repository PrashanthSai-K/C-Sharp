using System;

namespace TaskManagement_RESTAPI.Exceptions;

public class ConflictException : Exception
{
    public ConflictException(string message) : base (message)
    {
        
    }
}
