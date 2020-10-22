using AutoMapper;
using System;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web.Mvc;
using UsingRepository.Core;
using UsingRepository.Core.Models;
using UsingRepository.Core.ViewModels;
using WebGrease.Css.Extensions;

namespace UsingRepository.Areas.Admin.Controllers
{
    [Authorize(Roles = "Adminstrator,Create,Edit,Delete")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {
            var products = _unitOfWork.Products.GetproductsWithCategories();
            return View(products);
        }

        [AllowAnonymous]
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

        public ActionResult Create()
        {
            var productViewModel = new ProductViewModel()
            {
                Categories = new SelectList(_unitOfWork.Categories.GetCategoriesNotDeleted(), "Id", "Name"),
                Heading = "Create a Product"
            };
            return View("ProductForm", productViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Id,Name,ImagePath,Price,NumberInStock,AddedDate,Description,CategoryId,PostedImage")]
            ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                productViewModel.Categories =
                    new SelectList(_unitOfWork.Categories.GetCategoriesNotDeleted(), "Id", "Name");
                return View("ProductForm", productViewModel);
            }

            if (productViewModel.PostedImage != null)
                CutPostedImage(productViewModel);

            // auto Mapper
            var product = Mapper.Map<ProductViewModel, Product>(productViewModel);
            _unitOfWork.Products.Add(product);
            _unitOfWork.Complete();
            return RedirectToAction("Index");


        }

        public ActionResult Edit(int? id)
        {

            var product = GetProductFromDb(id);

            if (product is ActionResult)
                return product as ActionResult;

            var pro = product as Product;
            if (pro == null)
                return HttpNotFound();

            var productViewModel = Mapper.Map<ProductViewModel>(pro);
            productViewModel.Heading = "Edit a Product";
            productViewModel.Categories = new SelectList(_unitOfWork.Categories.GetCategoriesNotDeleted(),
                "Id", "Name", (product as Product).CategoryId);


            return View("ProductForm", productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
                    [Bind(Include = "Id,Name,ImagePath,Price,NumberInStock,AddedDate,Description,CategoryId,PostedImage,RowVersion")]
                    ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                productViewModel.Categories = new SelectList(_unitOfWork.Categories.Find(c => !c.IsDeleted), "Id",
                    "Name", productViewModel.CategoryId);
                return View("ProductForm", productViewModel);
            }


            var productInDb = GetProductFromDb(productViewModel.Id) as Product;

            if (productViewModel.PostedImage != null)
                CutPostedImage(productViewModel);
            else
                productViewModel.ImagePath = productInDb?.ImagePath;

            if (productInDb == null)
                return HttpNotFound();

            try
            {
                _unitOfWork.Products.Entry(productInDb).OriginalValues["RowVersion"] =
                    productViewModel.RowVersion;

                Mapper.Map(productViewModel, productInDb);

                _unitOfWork.Complete();
                return RedirectToAction("Index");

            }
            catch (DbUpdateConcurrencyException exception)
            {
                CatchCuncurrencyException(exception, productInDb);
                return Edit(productInDb.Id);
            }
        }

        private void CatchCuncurrencyException(DbUpdateConcurrencyException exception, Product productInDb)
        {
            var entry = exception.Entries.Single();
            var clientValues = (Product)entry.Entity;
            var databaseEntry = entry.GetDatabaseValues();

            var databaseValues = (Product)databaseEntry.ToObject();

            if (databaseValues.Name != clientValues.Name)
                ModelState.AddModelError("Name", @"Current Value : " + databaseValues.Name);
            if (databaseValues.Description != clientValues.Description)
                ModelState.AddModelError("Description", @"Current Value : " + databaseValues.Description);
            if (databaseValues.ImagePath != clientValues.ImagePath)
                ModelState.AddModelError("ImagePath", @"Current Value : " + databaseValues.ImagePath);
            if (databaseValues.AddedDate != clientValues.AddedDate)
                ModelState.AddModelError("AddedDate", @"Current Value : " + databaseValues.AddedDate);
            if (databaseValues.CategoryId != clientValues.CategoryId)
                ModelState.AddModelError("CategoryId", @"Current Value : " + databaseValues.CategoryId);
            if (databaseValues.NumberInStock != clientValues.NumberInStock)
                ModelState.AddModelError("NumberInStock", @"Current Value : " + databaseValues.NumberInStock);
            if (Math.Abs(databaseValues.Price - clientValues.Price) > 0)
                ModelState.AddModelError("Price", @"Current Value : " + databaseValues.Price);

            ModelState.AddModelError(string.Empty,
                @"The record you try to modify is updated by another one after you get the page."
                + @"if you want to modify it click save button again , or click back button");
            productInDb.RowVersion = databaseValues.RowVersion;
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

        private void CutPostedImage(ProductViewModel productViewModel)
        {
            string cutImageFileName = Path.Combine(Server.MapPath("~/Content/Images/CutImage"),
                productViewModel.PostedImage.FileName);
            productViewModel.PostedImage.SaveAs(cutImageFileName);
            try
            {
                Image newImage = new Bitmap(342, 342);
                using (var graphics = Graphics.FromImage(newImage))
                {
                    graphics.DrawImage(Image.FromFile(cutImageFileName),
                        new Rectangle(Point.Empty, newImage.Size));
                }
                var uploadImageFileName = Path.Combine(Server.MapPath("~/Content/Images/UploadImages"),
                    productViewModel.PostedImage.FileName);
                newImage.Save(uploadImageFileName);
                productViewModel.ImagePath = "~/Content/Images/UploadImages/" + productViewModel.PostedImage.FileName;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                #region Delete Files In Directory Test

                //code to delete image in test folder
                var filesInDirectory =
                    Path.Combine(Server.MapPath("~/Content/Images/CutImage"));

                var th = new Thread(() =>
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    Directory.GetFiles(filesInDirectory).ForEach(x => { System.IO.File.Delete(x); });
                });
                th.Start();

                #endregion
            }
        }

    }
}
