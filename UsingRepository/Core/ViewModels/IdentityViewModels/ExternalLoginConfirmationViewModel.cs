using System.ComponentModel.DataAnnotations;

namespace UsingRepository.Core.ViewModels.IdentityViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}