using System.ComponentModel.DataAnnotations;

namespace UsingRepository.Core.ViewModels
{
    public class CartViewModel
    {
        public int ProductId { get; set; }

        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        public int Quantity { get; set; }

        [Display(Name = "Unit Price")]
        public decimal Price { get; set; }

        public decimal Total => Price * Quantity;
    }
}