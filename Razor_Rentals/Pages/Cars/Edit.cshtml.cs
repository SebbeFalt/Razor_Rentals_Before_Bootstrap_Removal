using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Razor_Rentals.Data;
using Razor_Rentals.Data.Models;
using Razor_Rentals.Data.Repositories;
using Razor_Rentals.Helper;

namespace Razor_Rentals.Pages.Cars
{
    public class EditModel : PageModel
    {
        private readonly IGenericRepository<Car> _carRepository;
        private readonly IRoleChecker _roleChecker;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EditModel(IGenericRepository<Car> carRepository, IHttpContextAccessor httpContextAccessor, IRoleChecker roleChecker)
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
            if (isAdmin == true)
            {
                Car = car;
                return Page();
            }
            else
            {
                return RedirectToPage("/AccessDenied");
            }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public ActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }



            try
            {
                _carRepository.Update(Car);
                _carRepository.SaveChanges();
                TempData["Success"] = "Car edited successfully!";
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToPage("./Index");
        }
    }
}
