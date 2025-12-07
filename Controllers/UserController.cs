using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FOODCMS.API.Data;
using FOODCMS.API.Dtos;
using FOODCMS.API.Entities;

namespace FOODCMS.API.Controllers
{
    [ApiController]                         // บอกว่าเป็น API Controller
    [Route("api/[controller]")]             // => api/user (เพราะชื่อ class = UserController)
    public class UserController : ControllerBase
    {
        private readonly RestaurantDbContext _db;

        // รับ DbContext ผ่าน DI (AddDbContext ที่ Program.cs)
        public UserController(RestaurantDbContext db)
        {
            _db = db;
        }

        // POST api/user
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            // 1) validate ง่าย ๆ
            if (string.IsNullOrWhiteSpace(request.Username) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("Username และ Password ห้ามว่าง");
            }

            // 2) เช็ค user ซ้ำ
            var exists = await _db.Users.AnyAsync(u => u.Username == request.Username);
            if (exists)
            {
                return Conflict("มี Username นี้อยู่ในระบบแล้ว");
            }

            // 3) map จาก DTO -> Entity (ให้ตรงกับ User.cs + DbContext)
            var user = new User
            {
                Username = request.Username,
                // 👉 ตอนนี้ยังเก็บ plain text ก่อน (ทีหลังค่อยเปลี่ยนเป็น hash)
                PasswordHash = request.Password,
                FullName = request.FullName,
                Email = request.Email,
                Role = "user",
                IsActive = true,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "system" // หรือจะให้ null ก็ได้
            };

            // 4) เพิ่มลง DbSet แล้ว save
            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            // 5) เตรียมข้อมูลตอบกลับ (ไม่ควรส่ง password ออกไป)
            var result = new
            {
                user.UserId,
                user.Username,
                user.FullName,
                user.Email,
                user.Role,
                user.IsActive,
                user.CreatedOn
            };

            // 6) ส่ง 201 Created + body เป็น user ที่สร้าง
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, result);
        }

        // GET api/user/{id}  (ใช้คู่กับ CreatedAtAction ด้านบน)
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return NotFound();

            var result = new
            {
                user.UserId,
                user.Username,
                user.FullName,
                user.Email,
                user.Role,
                user.IsActive,
                user.CreatedOn
            };

            return Ok(result);
        }
    }
}
