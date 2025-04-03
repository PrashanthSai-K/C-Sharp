using System;

namespace task8.Class;

public class User
{
    public User(string name, string email, string mobile)
    {
        Name = name;
        Email = email;
        Mobile = mobile;
    }
    
    public string Name
    { get; set;}

    public string Email
    { get; set;}

    public string Mobile
    { get; set;}
}
