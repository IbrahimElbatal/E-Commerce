using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsingRepository.Core.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        [StringLength(500)]
        public string Comments { get; set; }

        [ForeignKey("Order")]
        [Required]
        public int OrderId { get; set; }

        [ForeignKey("Product")]
        [Required]
        public int ProductId { get; set; }

        [ForeignKey("User")]
        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}