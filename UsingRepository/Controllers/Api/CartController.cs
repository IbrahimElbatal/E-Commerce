using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;
using UsingRepository.Core;
using UsingRepository.Core.Dto;
using UsingRepository.Core.Models;
using UsingRepository.Core.ViewModels;

namespace UsingRepository.Controllers.Api
{
    [Authorize]
    public class CartController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }


        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult AddToCart(CartDto dto)
        {
            HttpSessionState session = HttpContext.Current.Session;

            List<CartViewModel> carts;

            if (dto.Id == null)
                return BadRequest();

            var product = _unitOfWork.Products.Get((int)dto.Id);

            if (product == null)
                return NotFound();

            var productDiscount = _unitOfWork.ProductDiscounts.GetProductDiscountNotDeleted()
                .SingleOrDefault(p => p.ProductId == product.Id);
            if (productDiscount != null)
            {
                product.Price = product.Price - (product.Price * productDiscount.Discount / 100);
            }
            var cart = new CartViewModel
            {
                ProductId = product.Id,
                Name = product.Name,
                ImagePath = product.ImagePath,
                Price = (decimal)product.Price,
                Quantity = int.Parse(dto.Quantity ?? "1")
            };

            if (session["cart"] == null)
            {
                carts = new List<CartViewModel>();
                carts.Add(cart);
                session["cart"] = carts;
            }
            else
            {
                carts = session["cart"] as List<CartViewModel>;

                //To see if this item exsists before      
                if (carts != null)
                {
                    var productInCart = carts.ToList().FirstOrDefault(c => c.ProductId == product.Id);

                    if (productInCart == null)
                        carts.Add(cart);
                    else
                        productInCart.Quantity += cart.Quantity;
                }

                session["cart"] = carts;
            }

            return Ok();
        }


        [HttpPost]
        public IHttpActionResult UpdateCart(UpdateCartDto cartDto)
        {
            HttpSessionState session = HttpContext.Current.Session;

            var carts = session["cart"] as List<CartViewModel>;

            if (carts == null)
                return NotFound();
            for (var i = 0; i < carts.Count; i++)
                carts[i].Quantity = cartDto.Quantity[i];
            if (cartDto.ChkCartRemove != null)
                carts.Where(c => cartDto.ChkCartRemove.Contains(c.ProductId)).ToList().ForEach(c => carts.Remove(c));

            session["cart"] = carts;

            return Ok();
        }



        [HttpPost]
        public IHttpActionResult ConfirmOrder1([FromBody]string comment)
        {
            var session = HttpContext.Current.Session;
            var carts = session["cart"] as List<CartViewModel>;
            if (carts != null)
            {
                var products = carts.Select(p => p.ProductId).ToList();
                var userName = User.Identity.Name;
                var userId = _unitOfWork.Users.Find(x => x.UserName.Equals(userName)).Select(u => u.Id).FirstOrDefault();

                var order = new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.Now
                };
                _unitOfWork.Orders.Add(order);

                var orderDetail = new OrderDetail();

                foreach (var cartViewModel in carts)
                {
                    orderDetail.ProductId = cartViewModel.ProductId;
                    orderDetail.OrderId = order.Id;
                    orderDetail.Quantity = cartViewModel.Quantity;
                    orderDetail.Comments = comment;
                    orderDetail.UserId = userId;

                    _unitOfWork.OrderDetails.Add(orderDetail);
                }

                foreach (var product in products)
                {
                    var pro = _unitOfWork.Products.Get(product);
                    if (pro != null)
                    {
                        var productQuantity = pro.NumberInStock;
                        productQuantity = productQuantity - orderDetail.Quantity;
                        pro.NumberInStock = productQuantity;
                    }
                }
                try
                {
                    _unitOfWork.Complete();
                    session.Clear();
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            return Ok();
        }


        #region Using Transaction

        [HttpPost]
        public IHttpActionResult ConfirmOrder([FromBody]string comment)
        {
            var session = HttpContext.Current.Session;
            var carts = session["cart"] as List<CartViewModel>;
            if (carts != null)
            {
                var products = carts.Select(p => p.ProductId).ToList();
                var userName = User.Identity.Name;
                var userId = _unitOfWork.Users.Find(x => x.UserName.Equals(userName)).Select(u => u.Id).FirstOrDefault();

                using (var transaction = new TransactionScope())
                {
                    var order = new Order
                    {
                        UserId = userId,
                        OrderDate = DateTime.Now
                    };
                    _unitOfWork.Orders.Add(order);
                    _unitOfWork.Complete();

                    var orderDetail = new OrderDetail();

                    foreach (var cartViewModel in carts)
                    {
                        orderDetail.ProductId = cartViewModel.ProductId;
                        orderDetail.OrderId = order.Id;
                        orderDetail.Quantity = cartViewModel.Quantity;
                        orderDetail.Comments = comment;
                        orderDetail.UserId = userId;

                        _unitOfWork.OrderDetails.Add(orderDetail);
                        _unitOfWork.Complete();


                    }

                    foreach (var product in products)
                    {
                        var pro = _unitOfWork.Products.Get(product);
                        if (pro != null)
                        {
                            var productQuantity = pro.NumberInStock;
                            productQuantity = productQuantity - orderDetail.Quantity;
                            pro.NumberInStock = productQuantity;
                            try
                            {
                                _unitOfWork.Complete();
                            }
                            catch (DbEntityValidationException e)
                            {
                                Console.WriteLine(e);
                                throw;
                            }

                        }
                    }
                    session.Clear();
                    transaction.Complete();
                }
            }
            return Ok();
        }

        #endregion
    }

}

