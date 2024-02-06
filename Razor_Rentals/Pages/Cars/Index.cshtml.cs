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

namespace Razor_Rentals.Pages.Cars
{
    public class IndexModel : PageModel
    {
        private readonly IGenericRepository<Car> _carRepository;
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IHttpContextAccessor HttpContextAccessor;

        public IndexModel(IGenericRepository<Car> carRepository,
                          IGenericRepository<Customer> customerRepository,
                          IHttpContextAccessor httpContextAccessor)
        {
            _carRepository = carRepository;
            _customerRepository = customerRepository;
            HttpContextAccessor = httpContextAccessor;
        }

        public IList<Car> Car { get; set; } = default!;

        public void OnGet()
        {
            Car = _carRepository.GetAll().ToList();
            string LoggedInEmail = HttpContextAccessor.HttpContext.Session.GetString("LoggedInEmail");
            //Check if string DOES contain something
            if (!string.IsNullOrEmpty(LoggedInEmail))
            {
                var customer = _customerRepository.Get(c => c.Email == LoggedInEmail);
                if (customer != null)
                {
                    int customerId = customer.CustomerId;
                    ViewData["CustomerId"] = customerId;
                }
            }
        }
    }
}
