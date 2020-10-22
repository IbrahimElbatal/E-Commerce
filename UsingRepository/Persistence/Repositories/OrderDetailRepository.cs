using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UsingRepository.Core.Models;
using UsingRepository.Core.Repositories;

namespace UsingRepository.Persistence.Repositories
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(HeroContext context) : base(context)
        {
        }

        public HeroContext HeroContext
        {
            get
            {
                return Context as HeroContext;
            }
        }

        public IEnumerable<OrderDetail> GetOrderDetailsWithProducts(int id)
        {
            return HeroContext.OrderDetails
                .Where(od => od.OrderId == id)
                .Include(p => p.Product)
                .ToList();
        }

        public IEnumerable<OrderDetail> GetOrderDetailsWithOrderAndProducts(string userId)
        {
            return HeroContext.OrderDetails
                .Where(od => od.UserId == userId)
                .Include(p => p.Product)
                .Include(o => o.Order)
                .ToList();
        }
    }
}