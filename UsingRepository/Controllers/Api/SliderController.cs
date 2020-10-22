using System.Web.Http;
using UsingRepository.Core;

namespace UsingRepository.Controllers.Api
{
    [Authorize(Roles = "Adminstrator,Delete")]
    public class SliderController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public SliderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var slider = _unitOfWork.Sliders.Get((int)id);

            if (slider == null || slider.IsDeleted)
                return NotFound();

            slider.IsDeleted = true;
            _unitOfWork.Complete();
            return Ok();
        }
    }
}
