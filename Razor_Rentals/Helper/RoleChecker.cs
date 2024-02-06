using Razor_Rentals.Data.Models;
using Razor_Rentals.Data.Repositories;

namespace Razor_Rentals.Helper
{
    public class RoleChecker : IRoleChecker
    {
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IGenericRepository<Admin> _adminRepository;

        public RoleChecker(IGenericRepository<Customer> customerRepository, IGenericRepository<Admin> adminRepository)
        {
            _customerRepository = customerRepository;
            _adminRepository = adminRepository;
        }
        public bool IsAdmin(string userEmail)
        {
            if (string.IsNullOrEmpty(userEmail))
            {
                return false; // No user is logged in
            }
            var customer = _customerRepository.Get(c => c.Email == userEmail);
            if (customer != null)
            {
                return false; // Not an admin
            }
            else
            {
                var admin = _adminRepository.Get(a => a.Email == userEmail);
                return true; // Admin is logged in
            }
        }
    }
}
