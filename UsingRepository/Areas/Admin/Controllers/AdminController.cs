
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UsingRepository.Areas.ViewModels;
using UsingRepository.Core;
using UsingRepository.Persistence;
using WebGrease.Css.Extensions;

namespace UsingRepository.Areas.Admin.Controllers
{
    [Authorize(Roles = "Adminstrator")]
    public class AdminController : Controller
    {
        // private fields for non automated property
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;

        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;


        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        public ApplicationSignInManager SignInManager
        {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>(); }
            private set { _signInManager = value; }
        }

        public AdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _roleManager = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(new HeroContext()));
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Roles()
        {
            return View(_unitOfWork.Roles.GetAll().ToList());
        }

        [HttpGet]
        public ActionResult RoleForm(string roleId)
        {
            if (string.IsNullOrWhiteSpace(roleId))
            {
                return View();
            }

            var role = _roleManager.FindById(roleId);
            if (role == null)
                return HttpNotFound();


            var model = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name,
                Users = UserManager.Users
                    .Where(u => u.Roles.Any(r => r.RoleId == roleId)).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleForm(RoleViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Id))
            {
                var role = new IdentityRole()
                {
                    Name = model.Name
                };
                _unitOfWork.Roles.Add(role);

            }
            else
            {
                var role = _roleManager.FindById(model.Id);
                if (role == null)
                    return HttpNotFound();

                role.Name = model.Name;
            }

            _unitOfWork.Complete();

            return RedirectToAction("Roles");
        }

        [HttpGet]
        public ActionResult AddUsersToRole(string roleId)
        {
            ViewBag.RoleId = roleId;

            var model = new List<UserRoleViewModel>();
            foreach (var user in UserManager.Users.ToList())
            {
                var userRole = new UserRoleViewModel()
                {
                    UserId = user.Id,
                    Email = user.Email,
                    IsSelected = user.Roles.Any(r => r.RoleId == roleId)
                };
                model.Add(userRole);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUsersToRole(string roleId, List<UserRoleViewModel> model)
        {
            var role = _roleManager.FindById(roleId);

            if (role == null)
                return HttpNotFound();

            role.Users.ForEach(u => { UserManager.RemoveFromRole(u.UserId, role.Name); });

            foreach (var userRole in model)
            {
                if (userRole.IsSelected)
                {
                    UserManager.AddToRole(userRole.UserId, role.Name);
                }
            }

            return RedirectToAction("RoleForm", new { roleId = roleId });
        }

        public ActionResult Users()
        {
            return View(_unitOfWork.Users.GetAll().ToList());
        }

        [HttpGet]
        public ActionResult EditUser(string userId)
        {
            var user = UserManager.FindById(userId);

            if (user == null)
                return HttpNotFound();
            var model = new EditUserViewModel()
            {
                Id = userId,
                UserName = user.UserName,
                Email = user.Email,
                Roles = UserManager.GetRoles(userId)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = UserManager.FindById(model.Id);

            if (user == null)
                return HttpNotFound();

            user.UserName = model.UserName;
            user.Email = model.Email;

            var result = UserManager.Update(user);
            if (result.Succeeded)
                return RedirectToAction("Users");
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult AddRolesToUser(string userId)
        {
            ViewBag.UserId = userId;

            var model = new List<UserRolesViewModel>();
            foreach (var role in _roleManager.Roles.ToList())
            {
                var userRoles = new UserRolesViewModel()
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    IsSelected = UserManager.IsInRole(userId, role.Name)
                };
                model.Add(userRoles);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult AddRolesToUser(string userId, List<UserRolesViewModel> model)
        {
            var user = UserManager.FindById(userId);

            if (user == null)
                return HttpNotFound();

            foreach (var role in _roleManager.Roles.ToList())
            {
                if (UserManager.IsInRole(userId, role.Name))
                {
                    UserManager.RemoveFromRole(userId, role.Name);
                }
            }

            foreach (var role in model)
            {
                if (role.IsSelected)
                {
                    UserManager.AddToRole(userId, role.RoleName);
                }
            }

            return RedirectToAction("EditUser", new { userId = userId });
        }


        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = User.Identity.GetUserId();

            var result = UserManager.ChangePassword(userId, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = UserManager.FindById(userId);
                if (user != null)
                {
                    SignInManager.SignIn(user, false, false);
                }
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = User.Identity.GetUserId();

            var code = UserManager.GenerateChangePhoneNumberToken(userId, model.Number);

            if (UserManager.EmailService != null)
            {
                var message = new IdentityMessage()
                {
                    Destination = UserManager.FindById(userId).Email,
                    Body = "Your Security Code is : " + code,
                    Subject = "Confirm Phone Number"
                };

                UserManager.EmailService.SendAsync(message);
            }

            return RedirectToAction("VerifyPhoneNumber", new { phoneNumber = model.Number });
        }

        [HttpGet]
        public ActionResult VerifyPhoneNumber(string phoneNumber)
        {
            var userId = User.Identity.GetUserId();

            //            var code = UserManager.GenerateChangePhoneNumberToken(userId, phoneNumber);

            var model = new VerifyPhoneNumberViewModel() { PhoneNumber = phoneNumber };
            return phoneNumber == null ? View("Error") : View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = User.Identity.GetUserId();

            var result = UserManager.ChangePhoneNumber(userId, model.PhoneNumber, model.Code);

            if (result.Succeeded)
            {
                var user = UserManager.FindById(userId);
                if (user != null)
                    SignInManager.SignIn(user, false, false);

                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult EnableTwoFactorAuthentication()
        {
            var userId = User.Identity.GetUserId();

            UserManager.SetTwoFactorEnabled(userId, true);

            var user = UserManager.FindById(userId);
            if (user != null)
                SignInManager.SignIn(user, false, false);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DisableTwoFactorAuthentication()
        {
            var userId = User.Identity.GetUserId();

            UserManager.SetTwoFactorEnabled(userId, false);

            var user = UserManager.FindById(userId);
            if (user != null)
                SignInManager.SignIn(user, false, false);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ManageTwoFactor()
        {
            var result = UserManager.GetTwoFactorEnabled(User.Identity.GetUserId());
            return View(result);
        }
        [HttpPost]
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("AllProducts", "Products", new { area = "" });
        }
    }
}