using UsingRepository.Core.Models;
using UsingRepository.Core.Repositories;

namespace UsingRepository.Persistence.Repositories
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(HeroContext context) : base(context)
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