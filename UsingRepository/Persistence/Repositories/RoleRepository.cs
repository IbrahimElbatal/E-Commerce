using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using UsingRepository.Core.Models;
using UsingRepository.Core.Repositories;

namespace UsingRepository.Persistence.Repositories
{
    class RoleRepository : Repository<IdentityRole>, IRoleRepository
    {
        public RoleRepository(DbContext context) : base(context)
        {
        }
        public HeroContext HeroContext
        {
            get
            {
                return Context as HeroContext;
            }
        }
    }
}