using System.Collections.Generic;
using UsingRepository.Core.Models;

namespace UsingRepository.Core.Repositories
{
    public interface IProductDiscountRepository : IRepository<ProductDiscount>
    {
        IEnumerable<int> GetProductsWithDiscount();
        IEnumerable<ProductDiscount> GetProductDiscountNotDeleted();
        IEnumerable<ProductDiscount> GetProductDiscountWithProduct();
    }
}