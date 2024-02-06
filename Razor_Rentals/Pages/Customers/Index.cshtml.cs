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
    public class IndexModel : PageModel
    {
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IRoleChecker _roleChecker;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(IGenericRepository<Customer> customerRepository, IHttpContextAccessor httpContextAccessor, IRoleChecker roleChecker)
        {
            _customerRepository = customerRepository;
            _httpContextAccessor = httpContextAccessor;
            _roleChecker = roleChecker;
        }

        public IList<Customer> Customer { get; set; } = default!;

        public ActionResult OnGet()
        {
            string userEmail = _httpContextAccessor.HttpContext.Session.GetString("LoggedInEmail");
            bool isAdmin = _roleChecker.IsAdmin(userEmail);
            if (isAdmin == true)
            {
                Customer = _customerRepository.GetAll().ToList();
                return Page();
            }
            else
            {
                return RedirectToPage("/AccessDenied");
            }
        }
    }
}
