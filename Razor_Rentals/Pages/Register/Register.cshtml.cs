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

namespace Razor_Rentals.Pages.Register
{
    public class RegisterModel : PageModel
    {
        private readonly IGenericRepository<Customer> _customerRepository;

        public RegisterModel(IGenericRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public ActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public ActionResult OnPost(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _customerRepository.Add(customer);
            _customerRepository.SaveChanges();
            TempData["Success"] = "Account created succesfully!";
            HttpContext.Session.SetString("LoggedInEmail", customer.Email);

            return RedirectToPage("/Cars/Index");
        }
    }
}
