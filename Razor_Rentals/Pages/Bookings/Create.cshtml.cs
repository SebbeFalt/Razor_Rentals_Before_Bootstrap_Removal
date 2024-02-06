using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Razor_Rentals.Data;
using Razor_Rentals.Data.Models;
using Razor_Rentals.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Razor_Rentals.Pages.Bookings
{
    public class CreateModel : PageModel
    {
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IGenericRepository<Car> _carRepository;
        private readonly IGenericRepository<Booking> _bookingRepository;
        public CreateModel(IGenericRepository<Booking> bookingRepository,
                           IGenericRepository<Car> carRepository,
                           IGenericRepository<Customer> customerRepository)
        {
            _bookingRepository = bookingRepository;
            _carRepository = carRepository;
            _customerRepository = customerRepository;
        }
        [BindProperty]
        public Booking Booking { get; set; }

        public ActionResult OnGet(int carId, int customerId)
        {


            Booking = new Booking
            {
                Car = _carRepository.Get(c => c.CarId == carId),
                Customer = _customerRepository.Get(c => c.CustomerId == customerId),
                CarPickupDate = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute),
                CarReturnDate = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute)
            };
           


            return Page();
        }



        public ActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Set the state of related entities
            _carRepository.SetEntityState(Booking.Car, EntityState.Unchanged);
            _customerRepository.SetEntityState(Booking.Customer, EntityState.Unchanged);
            _bookingRepository.Add(Booking);
            _bookingRepository.SaveChanges();
            TempData["Success"] = $"Booking receipt sent to {Booking.Customer.Email}!";



            return RedirectToPage("/Bookings/Index");
        }
    }
}
