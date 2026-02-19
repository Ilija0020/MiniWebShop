namespace MiniWebShop.Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }

        public UserProfile? Profile { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
