using System.Collections.Generic;
using UsingRepository.Core.Models;

namespace UsingRepository.Core.Repositories
{
    public interface ICountryRepository : IRepository<Country>
    {
        IEnumerable<Country> GetCountryWithCities();
    }
}