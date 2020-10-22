using System.Collections.Generic;
using UsingRepository.Core.Models;

namespace UsingRepository.Core.Repositories
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        IEnumerable<OrderDetail> GetOrderDetailsWithProducts(int id);
        IEnumerable<OrderDetail> GetOrderDetailsWithOrderAndProducts(string userId);
    }
}