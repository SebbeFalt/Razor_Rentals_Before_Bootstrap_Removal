using Razor_Rentals.Data.Models;

namespace Razor_Rentals.Data.Repositories
{
    public class AdminRepository : GenericRepository<Admin>
    {
        private readonly ApplicationDbContext _appDbCtx;
        public AdminRepository(ApplicationDbContext appDbCtx) : base(appDbCtx)
        {
            _appDbCtx = appDbCtx;
        }
    }
}
