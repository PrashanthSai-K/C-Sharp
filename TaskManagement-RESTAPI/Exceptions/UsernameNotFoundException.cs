using System;

namespace TaskManagement_RESTAPI.Exceptions;

public class UsernameNotFoundException : Exception
{
    public UsernameNotFoundException(string username) : base ($"Username : {username} not found.")
    {
    }
}
