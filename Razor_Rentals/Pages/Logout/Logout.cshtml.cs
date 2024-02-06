using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor_Rentals.Pages.Logout
{
    public class LogoutModel : PageModel
    {
        public ActionResult OnGet()
        {
            HttpContext.Session.SetString("LoggedInEmail", string.Empty);
            return Page();
        }
    }
}
