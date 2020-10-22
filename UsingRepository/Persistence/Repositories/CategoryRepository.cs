using System.Collections.Generic;
using System.Linq;
using UsingRepository.Core.Models;
using UsingRepository.Core.Repositories;

namespace UsingRepository.Persistence.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(HeroContext context) : base(context)
        {
        }

        public IEnumerable<Category> GetCategoriesNotDeleted()
        {
            return HeroContext.Categories
                .Where(c => !c.IsDeleted)
                .ToList();
        }

        public HeroContext HeroContext
        {
            get { return Context as HeroContext; }
        }


    }
}