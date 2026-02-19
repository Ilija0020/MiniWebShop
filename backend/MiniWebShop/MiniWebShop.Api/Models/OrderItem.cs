namespace MiniWebShop.Api.Models
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int AlbumId { get; set; }
        public int Quantity { get; set; }
        public double PriceAtTimeOfOrder { get; set; }

        public Album? Album { get; set; }
    }
}
