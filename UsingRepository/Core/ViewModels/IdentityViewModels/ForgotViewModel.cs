using System.ComponentModel.DataAnnotations;

namespace UsingRepository.Core.ViewModels.IdentityViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}