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
using Razor_Rentals.Helper;

namespace Razor_Rentals.Pages.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IRoleChecker _roleChecker;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DetailsModel(IGenericRepository<Customer> customerRepository, IHttpContextAccessor httpContextAccessor, IRoleChecker roleChecker)
        {
            _customerRepository = customerRepository;
            _httpContextAccessor = httpContextAccessor;
            _roleChecker = roleChecker;
        }

        public Customer Customer { get; set; } = default!;

        public ActionResult OnGet(int? id)
        {
            string userEmail = _httpContextAccessor.HttpContext.Session.GetString("LoggedInEmail");
            bool isAdmin = _roleChecker.IsAdmin(userEmail);
            if (id == null)
            {
                return NotFound();
            }

            var customer = _customerRepository.Get(a => a.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }
            if (isAdmin == true)
            {
                Customer = customer;
                return Page();
            }
            else
            {
                return RedirectToPage("/AccessDenied");
            }
        }
    }
}
