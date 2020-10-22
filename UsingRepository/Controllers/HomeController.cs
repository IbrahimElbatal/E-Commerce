using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;
using UsingRepository.Core;
using UsingRepository.Core.Models;

namespace UsingRepository.Controllers
{
    [Authorize(Roles = "Adminstrator")]
    public class HomeController : Controller
    {
        private ApplicationUserManager _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public HomeController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ActionResult About()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult SendMessage(Contact contact)
        {
            if (!ModelState.IsValid)
                return View("About", contact);

            _unitOfWork.Contacts.Add(contact);
            _unitOfWork.Complete();
            UserManager.SendEmail(User.Identity.GetUserId(), "FeedBack", contact.Messsage);
            return RedirectToAction("HomePage", "Products");
        }

    }
}