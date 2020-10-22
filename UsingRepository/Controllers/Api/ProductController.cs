using System.Linq;
using System.Web.Http;
using UsingRepository.Core;

namespace UsingRepository.Controllers.Api
{
    [Authorize(Roles = "Adminstrator,Delete")]
    public class ProductController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get(string term)
        {
            var products = term != null ?
                                _unitOfWork.Products.GetProductsNotDeleted()
                                .Where(p => p.Name.ToLower().Contains(term.ToLower()))
                                .ToList()
                                 : _unitOfWork.Products.GetProductsNotDeleted();


            return Ok(products);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var product = _unitOfWork.Products.Get((int)id);

            if (product == null || product.IsDeleted)
                return NotFound();

            product.IsDeleted = true;
            _unitOfWork.Complete();
            return Ok();
        }
    }
}
