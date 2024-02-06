using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor_Rentals.Data.Models;
using Razor_Rentals.Data.Repositories;

namespace Razor_Rentals.Pages.Login
{
    public class LoginViaButtonModel : PageModel
    {
        private readonly IGenericRepository<Customer> _customerRepository;
        public LoginViaButtonModel(IGenericRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public ActionResult OnGet()
        {
            return Page();
        }

        public ActionResult OnPost(string email, string password, int carId)
        {
            var customer = _customerRepository.Get(c => c.Email == email);

            if (customer == null)
            {
                // Customer does not exist for the provided email
                //ModelState.AddModelError(string.Empty, "Invalid email or password.");
                TempData["ErrorMessage"] = "Invalid email or password.";
                return Page();
            }
            else
            {
                if (customer.Password == password)
                {
                    // Password matches, proceed with redirection
                    HttpContext.Session.SetString("LoggedInEmail", customer.Email);
                    int customerId = customer.CustomerId;
                    return RedirectToPage("/Cars/Index");
                }
                else
                {
                    // Password does not match
                    //ModelState.AddModelError(string.Empty, "Invalid email or password.");
                    TempData["ErrorMessage"] = "Invalid email or password.";
                    return Page();
                }
            }
        }
    }
}
