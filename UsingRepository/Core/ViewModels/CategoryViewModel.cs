using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using UsingRepository.Areas.Admin.Controllers;
using UsingRepository.Resources;

namespace UsingRepository.Core.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Display(ResourceType = typeof(CategoryResource), Name = "Category")]
        [Required(ErrorMessageResourceType = typeof(CategoryResource),
            ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(CategoryResource),
            ErrorMessageResourceName = "MaxLength")]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public string Heading { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<CategoryController, ActionResult>> create =
                    c => c.Create();
                Expression<Func<CategoryController, ActionResult>> edit =
                    c => c.Edit(this.Id);

                var action = Id != 0 ? edit : create;
                return (action.Body as MethodCallExpression)?.Method.Name;
            }
        }
    }
}