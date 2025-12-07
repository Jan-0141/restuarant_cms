using Microsoft.EntityFrameworkCore;
using FOODCMS.API.Data;
using FOODCMS.API.Dtos.Auth;
using FOODCMS.API.Helpers;
using FOODCMS.API.Entities;
using FOODCMS.API.Dtos;

namespace FOODCMS.API.Services;

public class AuthService
{
    private readonly RestaurantDbContext _db;
    private readonly JwtTokenHelper _jwtHelper;

    public AuthService(RestaurantDbContext db, JwtTokenHelper jwtHelper)
    {
        _db = db;
        _jwtHelper = jwtHelper;
    }

        public async Task<User?> LoginAsync(LoginRequest request)
        {
            // 1) หา user จาก username
            var user = await _db.Users
                .SingleOrDefaultAsync(u => u.Username == request.Username);

            if (user == null)
                return null;

            // 2) ตรวจ password (plain text จาก request เทียบกับ hash ใน DB)
            var isValid = PasswordHasher.Verify(request.Password, user.PasswordHash);
            if (!isValid)
                return null;

            // 3) ok แล้วก็ return user กลับไปให้ controller เอาไป gen JWT ต่อ
            return user;
        }


    // public async Task RegisterAsync(RegisterRequest request)
    // {
    // var user = new User
    // {
    //     Username = request.Username,
    //     PasswordHash = PasswordHasher.Hash(request.Password) // 🔴 hash ก่อนเก็บ
    // };

    // _db.Users.Add(user);
    // await _db.SaveChangesAsync();
    // }
}
