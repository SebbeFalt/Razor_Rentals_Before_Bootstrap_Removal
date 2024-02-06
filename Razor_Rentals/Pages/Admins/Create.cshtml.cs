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

namespace Razor_Rentals.Pages.Admins
{
    public class CreateModel : PageModel
    {
        private readonly IGenericRepository<Admin> _adminRepository;
        private readonly IRoleChecker _roleChecker;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateModel(IGenericRepository<Admin> adminRepository, IHttpContextAccessor httpContextAccessor, IRoleChecker roleChecker)
        {
            _adminRepository = adminRepository;
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
        public Admin Admin { get; set; } = default!;

        public ActionResult OnPost(Admin admin)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            _adminRepository.Add(admin);
            _adminRepository.SaveChanges();
            TempData["Success"] = "Admin created successfully!";

            return RedirectToPage("./Index");
        }
    }
}
