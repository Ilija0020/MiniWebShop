namespace MiniWebShop.Api.Models
{
    public class Album
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public double Price { get; set; }
        public int ArtistId { get; set; }
        public Artist? Artist { get; set; }
    }
}
