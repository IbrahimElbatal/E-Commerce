using System.Linq;
using System.Web.Http;
using UsingRepository.Core;
using UsingRepository.Core.Models;

namespace UsingRepository.Controllers.Api
{
    public class UserController : ApiController
    {

        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetUser(string userName)
        {
            var user = _unitOfWork.Users.GetAll()
                 .SingleOrDefault(s => s.UserName.Equals(userName) || s.Email.Equals(userName));
            return Ok(user);
        }
        [HttpPost]
        [Authorize]
        public IHttpActionResult PostUser([FromUri]string id, ApplicationUser user)
        {
            if (id == null)
                return BadRequest();

            var employee = _unitOfWork.Users.Find(e => e.Id == id).Single();
            if (employee == null)
                return NotFound();

            employee.Address = user.Address;
            employee.CityId = user.CityId;
            employee.CountryId = user.CountryId;
            employee.Fax = user.Fax;
            employee.FirstName = user.FirstName;
            employee.LastName = user.LastName;
            employee.PostedCode = user.PostedCode;

            _unitOfWork.Complete();
            return Ok(employee);
        }

        [HttpDelete]
        public IHttpActionResult Delete(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return BadRequest();

            if (!User.Identity.IsAuthenticated &&
                (!User.IsInRole("Adminstrator") || !User.IsInRole("Delete")))
                return Unauthorized();

            var user = _unitOfWork.Users
                .Find(u => u.Id == userId).FirstOrDefault();

            if (user == null)
                return NotFound();

            _unitOfWork.Users.Remove(user);
            _unitOfWork.Complete();
            return Ok();
        }

    }
}
