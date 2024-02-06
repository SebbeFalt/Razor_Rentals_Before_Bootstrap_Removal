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
    public class IndexModel : PageModel
    {
        private readonly IGenericRepository<Admin> _adminRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRoleChecker _roleChecker;

        public IndexModel(IGenericRepository<Admin> adminRepository, IHttpContextAccessor httpContextAccessor, IRoleChecker roleChecker)
        {
            _adminRepository = adminRepository;
            _httpContextAccessor = httpContextAccessor;
            _roleChecker = roleChecker;
        }

        public IList<Admin> Admin { get; set; } = default!;

        public ActionResult OnGet()
        {
            string userEmail = _httpContextAccessor.HttpContext.Session.GetString("LoggedInEmail");
            bool isAdmin = _roleChecker.IsAdmin(userEmail);
            if (isAdmin == true)
            {
                Admin = _adminRepository.GetAll().ToList();
                return Page();
            }
            else
            {
                return RedirectToPage("/AccessDenied");
            }
            
        }
    }
}
