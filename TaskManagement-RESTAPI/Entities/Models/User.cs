using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement_RESTAPI.Entities.Models;

public class User
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Mobile { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; }

    public DateTime? ExpireTime { get; set; }
    public string? RefreshToken { get; set; }
    public IEnumerable<TaskItem>? TaskItems { get; set; }
}
