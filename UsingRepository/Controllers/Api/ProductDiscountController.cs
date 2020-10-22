using System.Web.Http;
using UsingRepository.Core;

namespace UsingRepository.Controllers.Api
{
    [Authorize(Roles = "Adminstrator,Delete")]
    public class ProductDiscountController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductDiscountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpDelete]
        public IHttpActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var category = _unitOfWork.ProductDiscounts.Get((int)id);

            if (category == null || category.IsDeleted)
                return NotFound();

            category.IsDeleted = true;
            _unitOfWork.Complete();

            return Ok();
        }
    }
}
