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

namespace Razor_Rentals.Pages.Cars
{
    public class CreateModel : PageModel
    {
        private readonly IGenericRepository<Car> _carRepository;
        private readonly IRoleChecker _roleChecker;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateModel(IGenericRepository<Car> carRepository, IHttpContextAccessor httpContextAccessor, IRoleChecker roleChecker)
        {
            _carRepository = carRepository;
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
        public Car Car { get; set; } = default!;

        public ActionResult OnPost(Car car)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _carRepository.Add(car);
            _carRepository.SaveChanges();
            TempData["Success"] = "Car created successfully!";

            return RedirectToPage("./Index");
        }
    }
}
