using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Mvc;

namespace UsingRepository.Core.Models
{
    public class Product
    {
        [Display(Name = "Product Code")]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Product Name")]
        public string Name { get; set; }


        [Display(Name = "Product Image")]
        public string ImagePath { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public float Price { get; set; }

        [Required]
        [Display(Name = "Number In Stock")]
        [Range(10, 200, ErrorMessage = "{0} Must Be Greater Than 10 and Less Than 200")]
        public int NumberInStock { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Product Date")]
        public DateTime AddedDate { get; set; }

        [StringLength(500)]
        [Display(Name = "Description")]
        [AllowHtml]
        public string Description { get; set; }


        [Required]
        [ForeignKey("Category")]
        [Display(Name = "Brand")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [NotMapped]
        public HttpPostedFileBase PostedImage { get; set; }

        public bool IsDeleted { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}