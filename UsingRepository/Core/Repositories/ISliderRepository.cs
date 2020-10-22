using System.Collections.Generic;
using UsingRepository.Core.Models;

namespace UsingRepository.Core.Repositories
{
    public interface ISliderRepository : IRepository<Slider>
    {
        IEnumerable<Slider> GetSliderNotDeleted();
    }
}