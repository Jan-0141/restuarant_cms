namespace FOODCMS.API.Dtos.Auth;

// body ที่ Flutter ส่งมาเวลา login
public class LoginRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}

