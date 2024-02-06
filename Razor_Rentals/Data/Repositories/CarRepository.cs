using Razor_Rentals.Data.Models;

namespace Razor_Rentals.Data.Repositories
{
    public class CarRepository : GenericRepository<Car>
    {
        private readonly ApplicationDbContext _appDbCtx;
        public CarRepository(ApplicationDbContext appDbCtx) : base(appDbCtx)
        {
            _appDbCtx = appDbCtx;
        }
    }
}
