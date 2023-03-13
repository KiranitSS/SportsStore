namespace SportsStore.Models
{
    public class Cart
    {
        private readonly List<CartLine> lines = new List<CartLine>();

        public IReadOnlyCollection<CartLine> Lines { get => this.lines; }

        public virtual void AddItem(Product product, int quantity)
        {
            CartLine? line = this.lines
                .FirstOrDefault(p => p.Product.ProductId == product.ProductId);

            if (line is null) 
            {
                this.lines.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity,
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Product product) 
        {
            this.lines.RemoveAll(l => l.Product.ProductId == product.ProductId);
        }

        public decimal ComputeTotalValue()
        {
            return this.lines.Sum(l => l.Product.Price * l.Quantity);
        }

        public virtual void Clear()
        {
            this.lines.Clear();
        }
    }
}
