using System.ComponentModel.DataAnnotations;
using UsingRepository.Resources;

namespace UsingRepository.Core.Models
{
    public class Category
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
    }
}