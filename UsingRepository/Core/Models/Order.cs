using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsingRepository.Core.Models
{
    public class Order
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        [ForeignKey("User")]
        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}