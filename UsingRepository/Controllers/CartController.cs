using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using UsingRepository.Core;
using UsingRepository.Core.ViewModels;

namespace UsingRepository.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public CartController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
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

        public ActionResult Index()
        {
            var productViewModel = new ProductViewModel
            {
                CategoryList = _unitOfWork.Categories.GetCategoriesNotDeleted(),
                Products = _unitOfWork.Products.GetRandomProducts(),
                RandomProducts = _unitOfWork.Products.GetRandomProducts()
            };

            if (Session["cart"] == null)
                return View("AddToCart", productViewModel);

            var carts = Session["cart"] as List<CartViewModel>;
            productViewModel.CartViewModels = carts;

            return View("AddToCart", productViewModel);
        }

        public ActionResult Checkout()
        {
            //            if (!ModelState.IsValid)
            //                return View(userName);
            //            var user = UserManager.FindByEmail(userName);
            //            if (user != null)
            //                return HttpNotFound();

            return View();
        }



    }
}