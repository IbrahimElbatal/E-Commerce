using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using UsingRepository.Areas.Admin.Controllers;
using UsingRepository.Resources;

namespace UsingRepository.Core.ViewModels
{
    public class SliderViewModel
    {
        public int Id { get; set; }
        [StringLength(200)]
        [Display(ResourceType = typeof(SliderResource), Name = "Image")]
        public string ImagePath { get; set; }

        [Display(ResourceType = typeof(SliderResource), Name = "Header")]
        [StringLength(250)]
        public string Header { get; set; }

        [Display(ResourceType = typeof(SliderResource), Name = "Paragraph")]
        [StringLength(250)]
        [AllowHtml]
        public string Paragraph { get; set; }
        [NotMapped]
        public HttpPostedFileBase PostedFileBase { get; set; }

        public string Heading { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<SliderController, ActionResult>> createExpression =
                    exp => exp.Create();
                Expression<Func<SliderController, ActionResult>> editExpression =
                    exp => exp.Edit(this);
                var action = Id != 0 ? editExpression : createExpression;

                return (action.Body as MethodCallExpression)?.Method.Name;
            }
        }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}