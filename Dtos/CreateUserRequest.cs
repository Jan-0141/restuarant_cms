namespace FOODCMS.API.Dtos
{
    public class CreateUserRequest
    {
        public string Username { get; set; }   // ลูกค้ากรอก
        public string Password { get; set; }   // ลูกค้ากรอก
        public string FullName { get; set; }   // ลูกค้ากรอก
        public string Email { get; set; }      // ลูกค้ากรอก
    }
}
