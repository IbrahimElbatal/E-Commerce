using System.Collections.Generic;
using UsingRepository.Core.Models;

namespace UsingRepository.Core.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> GetOrdersWithUsers();
    }
}