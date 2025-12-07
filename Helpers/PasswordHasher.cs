namespace FOODCMS.API.Helpers;

public static class PasswordHasher
{
    // ใช้ตอนสร้าง user ใหม่ / เปลี่ยนรหัสผ่าน
    public static string Hash(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);

    // ใช้ตอน login เพื่อเทียบ password กับ hash ที่อยู่ใน DB
    public static bool Verify(string password, string hash)
        => BCrypt.Net.BCrypt.Verify(password, hash);
}
