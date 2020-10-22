using AutoMapper;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using UsingRepository.Core;
using UsingRepository.Core.Models;
using UsingRepository.Core.ViewModels;

namespace UsingRepository.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Details(int? id)
        {
            var product = GetProductFromDb(id);
            var productDiscount = _unitOfWork.ProductDiscounts
                .GetProductDiscountNotDeleted().SingleOrDefault(p => p.ProductId == (product as Product).Id);
            if (productDiscount != null)
                ViewBag.discount = productDiscount.Discount;
            if (product is ActionResult)
                return product as ActionResult;

            //auto mapper
            var productViewModel = Mapper.Map<ProductViewModel>(product as Product);

            productViewModel.CategoryList = _unitOfWork.Categories.GetCategoriesNotDeleted();
            productViewModel.Products = _unitOfWork.Products.GetRelatedProductsNotDeleted(productViewModel.CategoryId);
            productViewModel.RandomProducts = _unitOfWork.Products.GetRandomProducts();

            return View(productViewModel);
        }

        public ActionResult HomePage()
        {
            var productViewModel = new ProductViewModel();
            productViewModel.Products = _unitOfWork.Products.GetProductsNotDeleted();
            productViewModel.Sliders = _unitOfWork.Sliders.GetSliderNotDeleted();

            return View(productViewModel);
        }

        public ActionResult AllProducts(int? page, string search, string category)
        {
            var pagedList = _unitOfWork.Products.GetProductsNotDeleted().ToPagedList(page ?? 1, 9);

            if (search != null)
                pagedList = _unitOfWork.Products.Find(p => p.Name.Contains(search) && !p.IsDeleted).ToPagedList(page ?? 1, 9);

            if (category != null)
                pagedList = _unitOfWork.Products.Find(p => p.Category.Name.Contains(category) && !p.IsDeleted).ToPagedList(page ?? 1, 9);

            var productViewModel =
                new ProductViewModel
                {
                    CategoryList = _unitOfWork.Categories.GetCategoriesNotDeleted(),
                    Products = pagedList,
                    RandomProducts = _unitOfWork.Products.GetRandomProducts()
                };

            return View(productViewModel);
        }

        public ActionResult BestSeller(int? page)
        {
            var products = _unitOfWork.ProductDiscounts.GetProductsWithDiscount();
            ViewBag.Discount = _unitOfWork.ProductDiscounts.GetProductDiscountNotDeleted();
            var listOfProducts = new List<Product>();
            var allproducts = _unitOfWork.Products.GetProductsNotDeleted().ToList();

            foreach (var productl in products)
            {
                var product = allproducts.FirstOrDefault(p => p.Id == productl);
                listOfProducts.Add(product);
            }

            var productViewModel = new ProductViewModel
            {
                Products = listOfProducts.ToPagedList(page ?? 1, 10),
                CategoryList = _unitOfWork.Categories.GetCategoriesNotDeleted(),
                RandomProducts = _unitOfWork.Products.GetRandomProducts(),
                Heading = "Best Seller"
            };
            return View("TopSeller", productViewModel);
        }

        public ActionResult TopSeller(int? page)
        {
            var products = _unitOfWork.OrderDetails.GetAll().GroupBy(p => p.ProductId).Select(p => new
            {
                ProductId = p.Key,
                NumOfOrder = p.Count()
            }).OrderByDescending(p => p.NumOfOrder).Take(10);

            var listOfProducts = new List<Product>();
            var allProducts = _unitOfWork.Products.GetProductsNotDeleted().ToList();

            foreach (var productl in products.ToList())
            {
                var product = allProducts.FirstOrDefault(p => p.Id == productl.ProductId);
                listOfProducts.Add(product);
            }

            var productViewModel = new ProductViewModel
            {
                Products = listOfProducts.ToPagedList(page ?? 1, 10),
                CategoryList = _unitOfWork.Categories.GetCategoriesNotDeleted(),
                RandomProducts = _unitOfWork.Products.GetRandomProducts(),
                Heading = "Top Seller"
            };
            return View(productViewModel);
        }
        private object GetProductFromDb(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Product product = _unitOfWork.Products.Get((int)id);

            if (product == null)
                return HttpNotFound();

            return product;
        }
    }
}
