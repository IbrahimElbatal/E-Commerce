using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UsingRepository.Core.Models;
using UsingRepository.Resources;

namespace UsingRepository.Areas.ViewModels
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {
            Users = new List<ApplicationUser>();
        }
        public string Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(UserResource)
            , ErrorMessageResourceName = "RequiredRoleName")]
        [Display(ResourceType = typeof(UserResource)
            , Name = "DisplayRoleName")]
        public string Name { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
    }
}