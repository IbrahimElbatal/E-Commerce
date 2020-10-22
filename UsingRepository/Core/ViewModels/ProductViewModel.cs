using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using UsingRepository.Areas.Admin.Controllers;
using UsingRepository.Core.Models;
using UsingRepository.Persistence;
using UsingRepository.Resources;

namespace UsingRepository.Core.ViewModels
{
    public class ProductViewModel
    {
        [Display(ResourceType = typeof(ProductResource)
            , Name = "ProductCode")]
        public int Id { get; set; }


        [Display(ResourceType = typeof(ProductResource)
            , Name = "DisplayName")]
        [Required(ErrorMessageResourceType = typeof(ProductResource),
            ErrorMessageResourceName = "RequiredName")]
        [StringLength(50, MinimumLength = 3,
            ErrorMessageResourceType = typeof(ProductResource),
            ErrorMessageResourceName = "MinMaxName")]
        public string Name { get; set; }


        [Display(ResourceType = typeof(ProductResource)
            , Name = "ProductImage")]
        public string ImagePath { get; set; }

        [Display(ResourceType = typeof(ProductResource)
            , Name = "DisplayPrice")]
        [Required(ErrorMessageResourceType = typeof(ProductResource),
            ErrorMessageResourceName = "RequiredPrice")]
        [DataType(DataType.Currency)]
        public float? Price { get; set; }

        [Display(ResourceType = typeof(ProductResource)
            , Name = "DisplayStock")]
        [Required(ErrorMessageResourceType = typeof(ProductResource),
            ErrorMessageResourceName = "RequiredStock")]
        [Range(10, 200, ErrorMessage = "{0} Must Be Greater Than 10 and Less Than 200")]
        public int? NumberInStock { get; set; }

//        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(ResourceType = typeof(ProductResource)
            , Name = "ProductDate")]
        public DateTime? AddedDate { get; set; }

        [Display(ResourceType = typeof(ProductResource)
            , Name = "DisplayDescription")]
        [Required(ErrorMessageResourceType = typeof(ProductResource),
            ErrorMessageResourceName = "RequiredDescription")]
        [StringLength(500, MinimumLength = 3,
            ErrorMessageResourceType = typeof(ProductResource),
            ErrorMessageResourceName = "MaxDescription")]
        public string Description { get; set; }


        [Display(ResourceType = typeof(ProductResource)
            , Name = "DisplayCategory")]
        [Required(ErrorMessageResourceType = typeof(ProductResource),
            ErrorMessageResourceName = "RequiredCategory")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }


        public HttpPostedFileBase PostedImage { get; set; }

        public bool IsDeleted { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<ProductController, ActionResult>> create =
                    c => c.Create(this);
                Expression<Func<ProductController, ActionResult>> edit =
                    c => c.Edit(this);

                var action = Id != 0 ? edit : create;
                return (action.Body as MethodCallExpression)?.Method.Name;

            }
        }

        public string Heading { get; set; }
        public Category Category { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Product> RandomProducts { get; set; }
        public IEnumerable<CartViewModel> CartViewModels { get; set; }
        public IEnumerable<Slider> Sliders { get; set; }

        public IEnumerable<Product> GetTop3BestSeller
        {
            get
            {
                return new HeroContext().ProductDiscounts.OrderByDescending(p => p.Discount).Select(p => p.Product)
                    .Take(3).ToList();
            }
        }
    }
}