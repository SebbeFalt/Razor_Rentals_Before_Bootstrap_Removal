using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor_Rentals.Data.Models;
using Razor_Rentals.Data.Repositories;

namespace Razor_Rentals.Pages.Login
{
    public class LoginViaButtonAdminModel : PageModel
    {
        private readonly IGenericRepository<Admin> _adminRepository;
        public LoginViaButtonAdminModel(IGenericRepository<Admin> adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public void OnGet()
        {
        }
        public ActionResult OnPost(string email, string password, int carId)
        {
            var admin = _adminRepository.Get(a => a.Email == email);

            if (admin == null)
            {
                // Customer does not exist for the provided email
                //ModelState.AddModelError(string.Empty, "Invalid email or password.");
                TempData["ErrorMessage"] = "Log in failed.";
                return Page();
            }
            else
            {
                if (admin.Password == password)
                {
                    // Password matches, proceed with redirection
                    HttpContext.Session.SetString("LoggedInEmail", admin.Email);
                    return RedirectToPage("/Index");
                }
                else
                {
                    // Password does not match
                    //ModelState.AddModelError(string.Empty, "Invalid email or password.");
                    TempData["ErrorMessage"] = "Log in failed.";
                    return Page();
                }
            }
        }
    }
}
