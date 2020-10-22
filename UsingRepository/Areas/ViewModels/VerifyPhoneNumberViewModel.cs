using System.ComponentModel.DataAnnotations;
using UsingRepository.Resources;

namespace UsingRepository.Areas.ViewModels
{
    public class VerifyPhoneNumberViewModel
    {
        [Required(ErrorMessageResourceType = typeof(UserResource)
            , ErrorMessageResourceName = "RequiredCode")]
        [Display(ResourceType = typeof(UserResource), Name = "DisplayCode")]
        public string Code { get; set; }
        public string PhoneNumber { get; set; }
    }
}