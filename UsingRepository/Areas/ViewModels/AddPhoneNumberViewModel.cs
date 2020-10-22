using System.ComponentModel.DataAnnotations;
using UsingRepository.Resources;

namespace UsingRepository.Areas.ViewModels
{
    public class AddPhoneNumberViewModel
    {
        [Required(ErrorMessageResourceType = typeof(UserResource)
            , ErrorMessageResourceName = "RequiredPhone")]
        [Display(ResourceType = typeof(UserResource), Name = "DisplayPhone")]
        [DataType(DataType.PhoneNumber)]
        public string Number { get; set; }
    }
}