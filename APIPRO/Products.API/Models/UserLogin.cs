namespace Products.API.Models
{
    public class UserLogin
    {
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
