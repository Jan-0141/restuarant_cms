namespace FOODCMS.API.Dtos.Auth;

// ข้อมูล user ที่จะส่งกลับไปให้ frontend ใช้แสดงชื่อ / role
public class UserInfoDto
{
    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    public string? FullName { get; set; }
    public string Role { get; set; } = null!;
}

