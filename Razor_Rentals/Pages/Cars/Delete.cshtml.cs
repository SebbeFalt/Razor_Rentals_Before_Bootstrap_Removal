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

namespace Razor_Rentals.Pages.Cars
{
    public class DeleteModel : PageModel
    {
        private readonly IGenericRepository<Car> _carRepository;
        private readonly IRoleChecker _roleChecker;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteModel(IGenericRepository<Car> carRepository, IHttpContextAccessor httpContextAccessor, IRoleChecker roleChecker)
        {
            _carRepository = carRepository;
            _httpContextAccessor = httpContextAccessor;
            _roleChecker = roleChecker;
        }

        [BindProperty]
        public Car Car { get; set; } = default!;

        public ActionResult OnGet(int? id)
        {
            string userEmail = _httpContextAccessor.HttpContext.Session.GetString("LoggedInEmail");
            bool isAdmin = _roleChecker.IsAdmin(userEmail);
            if (id == null)
            {
                return NotFound();
            }

            var car = _carRepository.Get(a => a.CarId == id);

            if (car == null)
            {
                return NotFound();
            }
            if(isAdmin == true)
            {
                Car = car;
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

            var car = _carRepository.Get(a => a.CarId == id);
            if (car != null)
            {
                Car = car;
                _carRepository.Remove(Car);
                _carRepository.SaveChanges();
                TempData["Success"] = "Car deleted successfully!";
            }

            return RedirectToPage("./Index");
        }
    }
}
