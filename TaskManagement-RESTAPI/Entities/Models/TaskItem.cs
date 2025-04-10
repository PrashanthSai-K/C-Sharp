using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement_RESTAPI.Entities.Models;

public class TaskItem
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime DueDate { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }
}
