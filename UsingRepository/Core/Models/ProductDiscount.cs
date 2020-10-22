using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Web.Mvc;
using UsingRepository.Areas.Admin.Controllers;
using UsingRepository.Resources;

namespace UsingRepository.Core.Models
{
    public class ProductDiscount
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(ProductDiscountResource)
            , ErrorMessageResourceName = "RequiredDiscount")]
        [Display(ResourceType = typeof(ProductDiscountResource)
            , Name = "DisplayDiscount")]
        public float Discount { get; set; }


        [Required(ErrorMessageResourceType = typeof(ProductResource)
            , ErrorMessageResourceName = "RequiredName")]
        [Display(ResourceType = typeof(ProductResource)
            , Name = "DisplayName")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public bool IsDeleted { get; set; }

        [NotMapped]
        public string Action
        {
            get
            {
                Expression<Func<ProductDiscountController, ActionResult>> createExpression =
                    pds => pds.Create();
                Expression<Func<ProductDiscountController, ActionResult>> editExpression =
                    pds => pds.Edit(this);
                var action = Id != 0 ? editExpression : createExpression;
                return (action.Body as MethodCallExpression)?.Method.Name;
            }
        }
        [NotMapped]
        public string Heading { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}