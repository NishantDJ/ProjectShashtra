namespace ProjectShashtra.Data
{
    public class RegisterDTO
    {
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } = "user";
    }
}
