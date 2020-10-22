using AutoMapper;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using UsingRepository.Core;
using UsingRepository.Core.Models;
using UsingRepository.Core.ViewModels;

namespace UsingRepository.Areas.Ar.Controllers
{
    [Authorize(Roles = "Adminstrator,Create,Edit,Delete")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {
            return View(_unitOfWork.Categories.GetCategoriesNotDeleted());
        }

        public ActionResult Create()
        {
            var categoryViewModel = new CategoryViewModel()
            {
                Heading = "إضافة صنف"
            };
            return View("CategoryForm", categoryViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
                return View("CategoryForm", categoryViewModel);

            var category = Mapper.Map<CategoryViewModel, Category>(categoryViewModel);

            _unitOfWork.Categories.Add(category);
            _unitOfWork.Complete();

            return RedirectToAction("Index");

        }

        public ActionResult Edit(int? id)
        {
            var category = GetCategoryFromDb(id);
            if (category is ActionResult)
                return category as ActionResult;

            var cat = category as Category;
            if (cat == null)
                return HttpNotFound();

            var categoryViewModel = Mapper.Map<CategoryViewModel>(cat);
            categoryViewModel.Heading = "تعديل صنف";

            return View("CategoryForm", categoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
                return View("CategoryForm", categoryViewModel);

            var categoryInDb = _unitOfWork.Categories.Get(categoryViewModel.Id);

            if (categoryInDb == null)
                ModelState.AddModelError("", @"Unable To Save Changes because Category deleted By Another one ");

            try
            {
                _unitOfWork.Categories.Entry(categoryInDb).OriginalValues["RowVersion"] =
                    categoryViewModel.RowVersion;

                Mapper.Map(categoryViewModel, categoryInDb);

                _unitOfWork.Complete();
                return RedirectToAction("Index");

            }
            catch (DbUpdateConcurrencyException exception)
            {
                CatchCuncurrencyException(exception, categoryInDb);
                return Edit(categoryInDb?.Id);
            }
        }


        private object GetCategoryFromDb(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var category = _unitOfWork.Categories.Get((int)id);

                if (category == null)
                    return HttpNotFound();
                return category;
            }
        }

        private void CatchCuncurrencyException(DbUpdateConcurrencyException exception, Category categoryInDb)
        {
            var entry = exception.Entries.Single();
            var clientValues = (Category)entry.Entity;
            var databaseEntry = entry.GetDatabaseValues();

            var databaseValues = (Category)databaseEntry.ToObject();
            if (databaseValues.Name != clientValues.Name)
                ModelState.AddModelError("Name", @"Current Value : " + databaseValues.Name);
            ModelState.AddModelError(string.Empty,
                @"The record you try to modify is updated by another one after you get the page."
                + @"if you want to modify it click save button again , or click back button");
            categoryInDb.RowVersion = databaseValues.RowVersion;
        }
        #region message forConcurency
        //        public ActionResult Index1()
        //
        //        {
        //            ViewBag.ConcurrencyErrorMessage = "The record you attempted to delete "
        //                                              + "was modified by another user after you got the original values. "
        //                                              + "The delete operation was canceled and the current values in the "
        //                                              + "database have been displayed. If you still want to delete this "
        //                                              + "record, click the Delete button again. Otherwise "
        //                                              + "click the Back to List hyperlink.";
        //            return View("Index", _unitOfWork.Categories.GetCategoriesNotDeleted());
        //        }

        #endregion
    }
}
