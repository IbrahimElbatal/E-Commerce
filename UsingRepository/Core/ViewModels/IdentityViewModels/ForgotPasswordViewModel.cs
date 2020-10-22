using System.ComponentModel.DataAnnotations;

namespace UsingRepository.Core.ViewModels.IdentityViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
