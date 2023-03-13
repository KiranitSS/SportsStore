namespace SportsStore.Models.Repository
{
    public class EFStoreRepository : IStoreRepository
    {
        private readonly StoreDbContext context;

        public EFStoreRepository(StoreDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Product> Products { get => this.context.Products; }

        public void CreateProduct(Product product)
        {
            this.context.Add(product);
            this.context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            this.context.Remove(product);
            this.context.SaveChanges();
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductId == 0)
            {
                this.context.Products.Add(product);
            }
            else
            {
                Product? dbEntry = this.context.Products?.FirstOrDefault(p => p.ProductId == product.ProductId);

                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }

            this.context.SaveChanges();
        }
    }
}
