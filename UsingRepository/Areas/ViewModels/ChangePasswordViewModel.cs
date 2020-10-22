using System.ComponentModel.DataAnnotations;
using UsingRepository.Resources;

namespace UsingRepository.Areas.ViewModels
{
    public class ChangePasswordViewModel
    {

        [Display(ResourceType = typeof(UserResource), Name = "DisplayCurrentPassword")]
        [Required(ErrorMessageResourceType = typeof(UserResource)
            , ErrorMessageResourceName = "RequiredCurrentPassword")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }


        [Display(ResourceType = typeof(UserResource), Name = "DisplayNewPassword")]
        [Required(ErrorMessageResourceType = typeof(UserResource)
            , ErrorMessageResourceName = "RequiredNewPassword")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }


        [Display(ResourceType = typeof(UserResource), Name = "DisplayConfirmPassword")]
        [Required(ErrorMessageResourceType = typeof(UserResource)
            , ErrorMessageResourceName = "RequiredConfirmPassword")]
        [Compare(otherProperty: "NewPassword",
            ErrorMessageResourceType = typeof(UserResource),
            ErrorMessageResourceName = "ComparePassword")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}