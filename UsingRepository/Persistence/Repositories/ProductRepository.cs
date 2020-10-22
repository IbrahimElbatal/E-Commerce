using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UsingRepository.Core.Models;
using UsingRepository.Core.Repositories;

namespace UsingRepository.Persistence.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(HeroContext context) : base(context)
        {
        }

        public IEnumerable<Product> GetRandomProducts()
        {

            return HeroContext.Products
                .Where(c => !c.IsDeleted)
                .OrderBy(p => Guid.NewGuid())
                .ToList();
        }

        public IEnumerable<Product> GetProductsNotDeleted()
        {
            return HeroContext.Products
                .Where(p => !p.IsDeleted)
                .ToList();
        }

        public IEnumerable<Product> GetproductsWithCategories()
        {
            return HeroContext.Products.
                Where(p => !p.IsDeleted)
                .Include(c => c.Category)
                .ToList();
        }
        public IEnumerable<Product> GetRelatedProductsNotDeleted(int categoryId)
        {
            return HeroContext.Products
                .Where(p => !p.IsDeleted && p.CategoryId == categoryId)
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