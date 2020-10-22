using System.ComponentModel.DataAnnotations;

namespace UsingRepository.Core.ViewModels.IdentityViewModels
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }
}