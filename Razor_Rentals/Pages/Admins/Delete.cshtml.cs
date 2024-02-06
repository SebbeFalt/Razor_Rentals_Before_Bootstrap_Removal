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

namespace Razor_Rentals.Pages.Admins
{
    public class DeleteModel : PageModel
    {
        private readonly IGenericRepository<Admin> _adminRepository;
        private readonly IRoleChecker _roleChecker;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteModel(IGenericRepository<Admin> adminRepository, IHttpContextAccessor httpContextAccessor, IRoleChecker roleChecker)
        {
            _adminRepository = adminRepository;
            _httpContextAccessor = httpContextAccessor;
            _roleChecker = roleChecker;
        }

        [BindProperty]
        public Admin Admin { get; set; } = default!;

        public ActionResult OnGet(int? id)
        {
            string userEmail = _httpContextAccessor.HttpContext.Session.GetString("LoggedInEmail");
            bool isAdmin = _roleChecker.IsAdmin(userEmail);
            if (id == null)
            {
                return NotFound();
            }

            var admin = _adminRepository.Get(a => a.AdminId == id);
            if (admin == null)
            {
                return NotFound();
            }

            if (isAdmin == true)
            {
                Admin = admin;
                return Page();
            }
            else
            {
                return RedirectToPage("/AccessDenied");
            }
        }

        public ActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = _adminRepository.Get(a => a.AdminId == id);
            if (admin != null)
            {
                Admin = admin;
                _adminRepository.Remove(Admin);
                _adminRepository.SaveChanges();
                TempData["Success"] = "Admin deleted successfully!";
            }

            return RedirectToPage("./Index");
        }
    }
}
