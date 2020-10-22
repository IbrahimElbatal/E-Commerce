using System.Collections.Generic;
using UsingRepository.Core.Models;

namespace UsingRepository.Core.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetProductsNotDeleted();
        IEnumerable<Product> GetRandomProducts();
        IEnumerable<Product> GetproductsWithCategories();
        IEnumerable<Product> GetRelatedProductsNotDeleted(int categoryId);
    }
}