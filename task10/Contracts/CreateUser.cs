using System;
using System.ComponentModel.DataAnnotations;

namespace task10.Contarcts;

public class CreateUser
{
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    public string? Role { get; set; }

}
