using System;

namespace TaskManagement_RESTAPI.Exceptions;

public class ErrorResponse
{
    public string? Message {get;set;}
    public int StatusCode {get;set;}

}
