using Razor_Rentals.Data.Models;

namespace Razor_Rentals.Data.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        private readonly ApplicationDbContext _appDbCtx;
        public CustomerRepository(ApplicationDbContext appDbCtx) : base(appDbCtx)
        {
            _appDbCtx = appDbCtx;
        }
    }
}
