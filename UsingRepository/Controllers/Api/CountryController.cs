using AutoMapper;
using System.Collections.Generic;
using System.Web.Http;
using UsingRepository.Core;
using UsingRepository.Core.Dto;
using UsingRepository.Core.Models;

namespace UsingRepository.Controllers.Api
{
    [Authorize]

    public class CountryController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public CountryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<CountryDto> GetCountries()
        {
            var x = _unitOfWork.Countries
                .GetCountryWithCities();
            return Mapper.Map<IEnumerable<Country>, IEnumerable<CountryDto>>(x);
        }
    }
}
