namespace SportsStore.Models
{
    public class CartLine
    {
        public int CartLineId { get; set; }

        public Product Product { get; set; } = new Product();

        public int Quantity { get; set; }
    }
}
