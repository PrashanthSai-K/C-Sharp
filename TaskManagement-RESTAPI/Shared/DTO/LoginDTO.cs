using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement_RESTAPI.Shared.DTO;

public class LoginDTO
{   
    [Required]
    [MinLength(3)]
    [MaxLength(12)]
    public required string UserName{get;set;}
    [Required]
    [MinLength(8)]
    [MaxLength(12)]
    public required string Password {get;set;}
}
