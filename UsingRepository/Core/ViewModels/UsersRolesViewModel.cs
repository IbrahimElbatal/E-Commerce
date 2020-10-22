using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using UsingRepository.Core.Models;
using UsingRepository.Persistence;

namespace UsingRepository.Core.ViewModels
{
    public class UsersRolesViewModel
    {
        public ApplicationUser User { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }

    }
}