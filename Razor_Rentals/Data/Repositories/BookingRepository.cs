using Razor_Rentals.Data.Models;

namespace Razor_Rentals.Data.Repositories
{
    public class BookingRepository : GenericRepository<Booking>
    {
        private readonly ApplicationDbContext _appDbCtx;

        public BookingRepository(ApplicationDbContext appDbCtx) : base(appDbCtx)
        {
            _appDbCtx = appDbCtx;
        }
        
       
    }
}
