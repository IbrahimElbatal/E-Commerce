using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UsingRepository.Core.Models;
using UsingRepository.Core.Repositories;

namespace UsingRepository.Persistence.Repositories
{
    public class ProductDiscountRepository : Repository<ProductDiscount>, IProductDiscountRepository
    {
        public ProductDiscountRepository(HeroContext context) : base(context)
        {
        }

        public IEnumerable<int> GetProductsWithDiscount()
        {
            return HeroContext.ProductDiscounts
                    .Where(pds => !pds.IsDeleted)
                      .Select(p => p.ProductId)
                      .ToList();
        }

        public IEnumerable<ProductDiscount> GetProductDiscountNotDeleted()
        {
            return HeroContext.ProductDiscounts
                .Where(pds => !pds.IsDeleted)
                .ToList();
        }

        public IEnumerable<ProductDiscount> GetProductDiscountWithProduct()
        {
            return HeroContext.ProductDiscounts
                .Where(pds => !pds.IsDeleted)
                .Include(p => p.Product)
                .ToList();
        }

        public HeroContext HeroContext
        {
            get
            {
                return Context as HeroContext;
            }
        }

    }
}