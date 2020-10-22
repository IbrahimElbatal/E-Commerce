using System.Collections.Generic;
using System.Linq;
using UsingRepository.Core.Models;
using UsingRepository.Core.Repositories;

namespace UsingRepository.Persistence.Repositories
{
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        public SliderRepository(HeroContext context) : base(context)
        {
        }

        public HeroContext HeroContext
        {
            get
            {
                return Context as HeroContext;
            }
        }

        public IEnumerable<Slider> GetSliderNotDeleted()
        {
            return HeroContext.Sliders
                .Where(s => !s.IsDeleted)
                .ToList();
        }
    }
}