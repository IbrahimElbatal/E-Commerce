using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UsingRepository.Resources;

namespace UsingRepository.Areas.ViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Roles = new List<string>();
        }
        [Display(ResourceType = typeof(UserResource), Name = "DisplayId")]
        public string Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(UserResource),
            ErrorMessageResourceName = "RequiredUserName")]
        [Display(ResourceType = typeof(UserResource), Name = "DisplayUserName")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(UserResource),
            ErrorMessageResourceName = "RequiredEmail")]
        [Display(ResourceType = typeof(UserResource), Name = "DisplayEmail")]
        [EmailAddress]
        public string Email { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}