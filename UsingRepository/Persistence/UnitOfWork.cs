using UsingRepository.Core;
using UsingRepository.Core.Repositories;
using UsingRepository.Persistence.Repositories;

namespace UsingRepository.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HeroContext _context;

        public UnitOfWork(HeroContext context)
        {
            _context = context;
            Contacts = new ContactRepository(_context);
            Users = new UserRepository(context);
            Roles = new RoleRepository(context);
            Orders = new OrderRepository(context);
            OrderDetails = new OrderDetailRepository(context);
            Products = new ProductRepository(_context);
            Categories = new CategoryRepository(_context);
            Sliders = new SliderRepository(_context);
            ProductDiscounts = new ProductDiscountRepository(_context);
            Countries = new CountryRepository(_context);
        }


        public IProductRepository Products { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IUserRepository Users { get; private set; }
        public IRoleRepository Roles { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IOrderDetailRepository OrderDetails { get; private set; }

        public ISliderRepository Sliders { get; private set; }
        public IContactRepository Contacts { get; private set; }
        public IProductDiscountRepository ProductDiscounts { get; private set; }
        public ICountryRepository Countries { get; private set; }


        public void Dispose()
        {
            _context.Dispose();
        }
        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}