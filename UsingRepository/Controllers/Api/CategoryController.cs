using System.Web.Http;
using UsingRepository.Core;

namespace UsingRepository.Controllers.Api
{
    [Authorize(Roles = "Adminstrator,Delete")]

    public class CategoryController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            if (!User.Identity.IsAuthenticated && (!User.IsInRole("Adminstrator") || !User.IsInRole("Delete")))
                return Unauthorized();

            var category = _unitOfWork.Categories.Get((int)id);

            if (category == null || category.IsDeleted)
                return NotFound();

            category.IsDeleted = true;
            _unitOfWork.Complete();
            return Ok();

            #region handle concurrency

            //            try
            //            {
            //                if (!rowVersion.Equals(Convert.ToBase64String(category.RowVersion)))
            //                    _unitOfWork.Categories.Entry(category).OriginalValues["RowVersion"] = null;
            //                else
            //                    _unitOfWork.Categories.Entry(category).OriginalValues["RowVersion"] =
            //                        category.RowVersion;
            //
            //                category.IsDeleted = true;
            //                _unitOfWork.Complete();
            //                return Ok();
            //            }
            //            catch (DbUpdateConcurrencyException)
            //            {
            //                string url = Url.Link("DeleteRoute", new
            //                {
            //                    Controller = "Categories",
            //                    action = "Index1",
            //                    concurrencyError = true
            //                });
            //                return Redirect(url);
            //            }

            #endregion
        }

    }
}
