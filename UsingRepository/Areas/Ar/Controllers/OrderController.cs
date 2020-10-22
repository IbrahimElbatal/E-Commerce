using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using UsingRepository.Core;

namespace UsingRepository.Areas.Ar.Controllers
{
    [Authorize(Roles = "Adminstrator")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Order()
        {
            var order = _unitOfWork.Orders
                .GetOrdersWithUsers();
            return View(order);
        }
        public ActionResult OrderDetails(int id)
        {
            var orderDetails = _unitOfWork.OrderDetails
                .GetOrderDetailsWithProducts(id);
            return View(orderDetails);
        }
        [AllowAnonymous]
        [Authorize]
        public ActionResult UserOrdersDetails()
        {
            var userId = User.Identity.GetUserId();
            var ordersDetails = _unitOfWork.OrderDetails
                .GetOrderDetailsWithOrderAndProducts(userId);
            return View(ordersDetails);
        }
    }
}