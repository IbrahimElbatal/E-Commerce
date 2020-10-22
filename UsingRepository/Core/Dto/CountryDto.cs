using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UsingRepository.Core.Dto
{
    public class CountryDto
    {
        public CountryDto()
        {
            Cities = new Collection<CityDto>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CityDto> Cities { get; set; }
    }
}