using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UsingRepository.Core.Models;
using UsingRepository.Core.Repositories;

namespace UsingRepository.Persistence.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(HeroContext context) : base(context)
        {
        }

        public HeroContext HeroContext
        {
            get
            {
                return Context as HeroContext;
            }
        }

        public IEnumerable<Order> GetOrdersWithUsers()
        {
            return HeroContext.Orders
                    .Distinct()
                    .Include(u => u.User)
                    .ToList();

        }
    }
}