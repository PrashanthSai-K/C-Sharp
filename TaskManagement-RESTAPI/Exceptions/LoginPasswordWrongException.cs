using System;

namespace TaskManagement_RESTAPI.Exceptions;

public class LoginPasswordWrongException : Exception
{
    public LoginPasswordWrongException() : base ("Password is incorrect.")
    {

    }
}
