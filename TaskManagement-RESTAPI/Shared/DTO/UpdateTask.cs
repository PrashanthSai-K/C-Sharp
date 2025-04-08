using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement_RESTAPI.Shared.DTO;

public class UpdateTask
{
    [Required]
    public int Id {get; set;}
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Description { get; set; }
    [Required]
    public bool IsCompleted { get; set; }
    [Required]
    public DateTime DueDate { get; set; }

}
