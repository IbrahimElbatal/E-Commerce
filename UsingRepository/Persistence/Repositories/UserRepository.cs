using UsingRepository.Core.Models;
using UsingRepository.Core.Repositories;

namespace UsingRepository.Persistence.Repositories
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        public UserRepository(HeroContext context) : base(context)
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