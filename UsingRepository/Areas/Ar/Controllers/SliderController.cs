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

namespace UsingRepository.Areas.Ar.Controllers
{
    [Authorize(Roles = "Adminstrator,Create,Edit,Delete")]
    public class SliderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public SliderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            return View(_unitOfWork.Sliders.GetSliderNotDeleted());
        }
        public ActionResult Create()
        {
            var sliderViewModel = new SliderViewModel()
            {
                Heading = "سلايدر جديد"
            };
            return View("SliderForm", sliderViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Id,ImagePath,Header,Paragraph,PostedFileBase")] SliderViewModel sliderViewModel)
        {
            if (!ModelState.IsValid)
                return View("SliderForm", sliderViewModel);

            if (sliderViewModel.PostedFileBase != null)
                CutImage(sliderViewModel);

            var slider = Mapper.Map<Slider>(sliderViewModel);

            _unitOfWork.Sliders.Add(slider);
            _unitOfWork.Complete();
            return RedirectToAction("Index");

        }

        public ActionResult Edit(int? id)
        {
            var slider = GetSliderFromDb(id);
            if (slider is ActionResult)
                return slider as ActionResult;

            var sliderViewModel = Mapper.Map<SliderViewModel>(slider as Slider);
            sliderViewModel.Heading = "تعديل السلايدر";

            return View("SliderForm", sliderViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id,ImagePath,Header,Paragraph,PostedFileBase,RowVersion")] SliderViewModel sliderViewModel)
        {
            if (!ModelState.IsValid)
                return View("SliderForm", sliderViewModel);

            var sliderInDb = GetSliderFromDb(sliderViewModel.Id) as Slider;
            if (sliderInDb == null)
                return HttpNotFound();

            if (sliderViewModel.PostedFileBase != null)
                CutImage(sliderViewModel);
            else
                sliderViewModel.ImagePath = sliderInDb.ImagePath;


            try
            {
                _unitOfWork.Sliders.Entry(sliderInDb).OriginalValues["RowVersion"] =
                    sliderViewModel.RowVersion;

                Mapper.Map(sliderViewModel, sliderInDb);

                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException exception)
            {
                CatchCuncurrencyException(exception, sliderInDb);
                return Edit(sliderInDb.Id);
            }
        }
        private void CatchCuncurrencyException(DbUpdateConcurrencyException exception, Slider sliderInDb)
        {
            var entry = exception.Entries.Single();
            var clientValues = (Slider)entry.Entity;
            var databaseEntry = entry.GetDatabaseValues();

            var databaseValues = (Slider)databaseEntry.ToObject();
            if (databaseValues.Header != clientValues.Header)
                ModelState.AddModelError("Header", @"Current Value : " + databaseValues.Header);
            if (databaseValues.Paragraph != clientValues.Paragraph)
                ModelState.AddModelError("Paragraph", @"Current Value : " + databaseValues.Paragraph);
            if (databaseValues.ImagePath != clientValues.ImagePath)
                ModelState.AddModelError("ImagePath", @"Current Value : " + databaseValues.ImagePath);

            ModelState.AddModelError(string.Empty,
                @"The record you try to modify is updated by another one after you get the page."
                + @"if you want to modify it click save button again , or click back button");
            sliderInDb.RowVersion = databaseValues.RowVersion;
        }
        private object GetSliderFromDb(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var slider = _unitOfWork.Sliders.Get((int)id);

            if (slider == null)
                return HttpNotFound();

            return slider;

        }
        private void CutImage(SliderViewModel sliderViewModel)
        {
            try
            {

                string fileName = Path.Combine(Server.MapPath("~/Content/Images/CutImage"), sliderViewModel.PostedFileBase.FileName);
                sliderViewModel.PostedFileBase.SaveAs(fileName);

                var newImage = new Bitmap(960, 330);
                using (var graphics = Graphics.FromImage(newImage))
                {
                    graphics.DrawImage(Image.FromFile(fileName),
                        new Rectangle(Point.Empty, newImage.Size));
                }

                string saveFileName = Path.Combine(Server.MapPath("~/Content/Images/SliderImages"), sliderViewModel.PostedFileBase.FileName);

                newImage.Save(saveFileName);
                sliderViewModel.ImagePath = "~/Content/Images/SliderImages/" + sliderViewModel.PostedFileBase.FileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            finally
            {
                var thread = new Thread(t =>
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    var filesInDirectory =
                        Path.Combine(Server.MapPath("~/Content/Images/CutImage"));
                    Directory.GetFiles(filesInDirectory).ForEach(System.IO.File.Delete);

                });
                thread.Start();
            }
        }
    }
}
