using System;
using UsingRepository.Core.Repositories;

namespace UsingRepository.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        IOrderRepository Orders { get; }
        IOrderDetailRepository OrderDetails { get; }
        ISliderRepository Sliders { get; }
        IContactRepository Contacts { get; }
        IProductDiscountRepository ProductDiscounts { get; }
        ICountryRepository Countries { get; }
        void Complete();

    }
}