namespace FOODCMS.API.Dtos.Auth;

// response เวลา login สำเร็จ
public class LoginResponse
{
    public string Token { get; set; } = null!;
    public UserInfoDto User { get; set; } = null!;
}
