using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Razor_Rentals.Data;
using Razor_Rentals.Data.Models;
using Razor_Rentals.Data.Repositories;

namespace Razor_Rentals.Pages.Bookings
{
    public class IndexModel : PageModel
    {
        private readonly IGenericRepository<Booking> _bookingRepository;


        public IndexModel(IGenericRepository<Booking> bookingRepository)
        {
            _bookingRepository = bookingRepository;

        }

        public IList<Booking> Booking { get; set; } = default!;

        public void OnGet()
        {
            //.Include customer & car info
            Booking = _bookingRepository.GetAllIncluding(b => b.Customer, b => b.Car)
                                        .OrderByDescending(b => b.BookingDate)
                                        .ToList();

        }
    }
}
