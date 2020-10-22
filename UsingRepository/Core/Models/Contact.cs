using System.ComponentModel.DataAnnotations;

namespace UsingRepository.Core.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(150)]
        public string EmailAddress { get; set; }
        [Required]
        [StringLength(500)]
        public string Messsage { get; set; }
    }
}