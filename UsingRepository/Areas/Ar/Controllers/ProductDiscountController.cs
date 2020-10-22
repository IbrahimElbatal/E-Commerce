using AutoMapper;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using UsingRepository.Core;
using UsingRepository.Core.Models;

namespace UsingRepository.Areas.Ar.Controllers
{
    [Authorize(Roles = "Adminstrator,Create")]
    public class ProductDiscountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductDiscountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var productDiscounts = _unitOfWork.ProductDiscounts.GetProductDiscountWithProduct();

            foreach (var productDiscount in productDiscounts)
                productDiscount.Product = _unitOfWork.Products
                                            .Get(productDiscount.ProductId);

            return View(productDiscounts);
        }

        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(
                _unitOfWork.Products.GetProductsNotDeleted(), "Id", "Name");
            var productDiscount = new ProductDiscount
            {
                Heading = "إضافة خصم جديد"
            };
            return View("ProductDiscountForm", productDiscount);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductDiscount productDiscount)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProductId = new SelectList(
                    _unitOfWork.Products.GetProductsNotDeleted(), "Id", "Name", productDiscount.ProductId);
                return View("ProductDiscountForm", productDiscount);
            }


            if (_unitOfWork.ProductDiscounts.GetProductDiscountNotDeleted().Contains(productDiscount))
                _unitOfWork.ProductDiscounts.Remove(productDiscount);
            _unitOfWork.ProductDiscounts.Add(productDiscount);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            var productDiscount = GetProductDiscountFromDb(id);
            if (productDiscount is ActionResult)
                return productDiscount as ActionResult;

            ViewBag.ProductId = new SelectList(
                _unitOfWork.Products.GetProductsNotDeleted(), "Id", "Name", (productDiscount as ProductDiscount)?.ProductId);

            (productDiscount as ProductDiscount).Heading = "تعديل الخصم";
            return View("ProductDiscountForm", productDiscount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductDiscount productDiscount)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProductId = new SelectList(
                    _unitOfWork.Products.GetProductsNotDeleted(), "Id", "Name", productDiscount.ProductId);
                return View("ProductDiscountForm", productDiscount);

            }
            var pDiscountInDb = GetProductDiscountFromDb(productDiscount.Id);

            try
            {
                _unitOfWork.ProductDiscounts.Entry(pDiscountInDb as ProductDiscount).OriginalValues["RowVersion"] =
                    productDiscount.RowVersion;

                Mapper.Map(productDiscount, pDiscountInDb);

                _unitOfWork.Complete();
                return RedirectToAction("Index");

            }
            catch (DbUpdateConcurrencyException exception)
            {
                CatchCuncurrencyException(exception, pDiscountInDb as ProductDiscount);
                return Edit((pDiscountInDb as ProductDiscount)?.Id);
            }


        }
        private void CatchCuncurrencyException(DbUpdateConcurrencyException exception, ProductDiscount pDiscountInDb)
        {
            var entry = exception.Entries.Single();
            var clientValues = (ProductDiscount)entry.Entity;
            var databaseEntry = entry.GetDatabaseValues();

            var databaseValues = (ProductDiscount)databaseEntry.ToObject();

            if (databaseValues.ProductId != clientValues.ProductId)
                ModelState.AddModelError("ProductId", @"Current Value : " + databaseValues.ProductId);
            if (Math.Abs(databaseValues.Discount - clientValues.Discount) > 0)
                ModelState.AddModelError("Discount", @"Current Value : " + databaseValues.Discount);

            ModelState.AddModelError(string.Empty,
                @"The record you try to modify is updated by another one after you get the page."
                + @"if you want to modify it click save button again , or click back button");

            pDiscountInDb.RowVersion = databaseValues.RowVersion;
        }
        private object GetProductDiscountFromDb(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var productDiscount = _unitOfWork.ProductDiscounts.Get((int)id);

            if (productDiscount == null)
                return HttpNotFound();

            return productDiscount;
        }
    }
}
