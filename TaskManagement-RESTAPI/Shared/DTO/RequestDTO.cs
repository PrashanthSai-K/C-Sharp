using System;

namespace TaskManagement_RESTAPI.Shared.DTO;

public class RequestDTO
{
    public string? RefreshToken { get; set; }
    public string? AccessToken { get; set; }
}
