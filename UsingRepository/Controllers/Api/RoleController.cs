using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UsingRepository.Core;

namespace UsingRepository.Controllers.Api
{
    public class RoleController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult Delete(string roleId)
        {
            if (string.IsNullOrWhiteSpace(roleId))
                return BadRequest();

            if (!User.Identity.IsAuthenticated &&
                (!User.IsInRole("Adminstrator") || !User.IsInRole("Delete")))
                return Unauthorized();

            var role = _unitOfWork.Roles
                .Find(r=>r.Id ==roleId).FirstOrDefault();

            if (role == null)
                return NotFound();

            _unitOfWork.Roles.Remove(role);
            _unitOfWork.Complete();
            return Ok();
        }

    }
}
