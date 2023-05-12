namespace VendomaticApi.Domain.Products.Services;

using VendomaticApi.Domain.Products;
using VendomaticApi.Databases;
using VendomaticApi.Services;

public interface IProductRepository : IGenericRepository<Product>
{
}

public sealed class ProductRepository : GenericRepository<Product>, IProductRepository
{
    private readonly VendomaticDbContext _dbContext;

    public ProductRepository(VendomaticDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
