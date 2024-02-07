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
    public class DeleteModel : PageModel
    {
        private readonly IGenericRepository<Booking> _bookingRepository;
        private readonly IGenericRepository<Customer> _customerRepository;

        public DeleteModel(IGenericRepository<Booking> bookingRepository, IGenericRepository<Customer> customerRepository)
        {
            _bookingRepository = bookingRepository;
            _customerRepository = customerRepository;
        }

        [BindProperty]
        public Booking Booking { get; set; } = default!;

        public ActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            //var booking = _bookingRepository.Get(b => b.BookingId == id);
            var booking = _bookingRepository.GetAllIncluding(b => b.Customer)
                                            .FirstOrDefault(b => b.BookingId == id);

            if (booking == null)
            {
                return NotFound();
            }
            else
            {
                Booking = booking;
            }
            return Page();
        }

        public ActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = _bookingRepository.Get(a => a.BookingId == id);
            if (booking != null)
            {
                Booking = booking;
                _bookingRepository.Remove(Booking);
                _bookingRepository.SaveChanges();
                TempData["Success"] = "Booking deleted successfully!";
            }

            return RedirectToPage("./Index");
        }
    }
}
