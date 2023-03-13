namespace SportsStore.Models.Repository
{
    public interface IStoreRepository
    {
        IQueryable<Product> Products { get; }

        void SaveProduct(Product product);

        void CreateProduct(Product product);

        void DeleteProduct(Product product);
    }
}
