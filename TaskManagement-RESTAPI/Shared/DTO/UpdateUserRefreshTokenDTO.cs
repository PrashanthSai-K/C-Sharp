using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement_RESTAPI.Shared.DTO;

public class UpdateUserRefreshTokenDTO
{
    [Required]
    public int Id { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(12)]
    public string? Username { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    [Phone]
    public string? Mobile { get; set; }
    [Required]
    [MinLength(8)]
    [MaxLength(12)]
    public string? Password { get; set; }
    [Required]
    public string? Role { get; set; }


    [Required]
    public string? RefreshToken { get; set; }

    [Required]
    public DateTime? ExpireTime { get; set; }

}
