using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UsingRepository.Core.Models;
using UsingRepository.Core.Repositories;

namespace UsingRepository.Persistence.Repositories
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(DbContext context) : base(context)
        {
        }
        public IEnumerable<Country> GetCountryWithCities()
        {
            return HeroContext.Countries
                .Include(c => c.Cities)
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