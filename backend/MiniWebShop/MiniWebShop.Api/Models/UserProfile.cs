namespace MiniWebShop.Api.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? ShippingAddress { get; set; }
        public string? FavoriteGenre { get; set; }
    }
}
