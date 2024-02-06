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
using Razor_Rentals.Helper;

namespace Razor_Rentals.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IRoleChecker _roleChecker;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateModel(IGenericRepository<Customer> customerRepository, IHttpContextAccessor httpContextAccessor, IRoleChecker roleChecker)
        {
            _customerRepository = customerRepository;
            _httpContextAccessor = httpContextAccessor;
            _roleChecker = roleChecker;
        }

        public ActionResult OnGet()
        {
            string userEmail = _httpContextAccessor.HttpContext.Session.GetString("LoggedInEmail");
            bool isAdmin = _roleChecker.IsAdmin(userEmail);
            if (isAdmin == true)
            {
                return Page();
            }
            else
            {
                return RedirectToPage("/AccessDenied");
            }
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public ActionResult OnPost(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _customerRepository.Add(customer);
            _customerRepository.SaveChanges();
            TempData["Success"] = "Customer created successfully!";

            return RedirectToPage("./Index");
        }
    }
}
