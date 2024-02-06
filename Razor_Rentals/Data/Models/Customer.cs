using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Razor_Rentals.Data.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [Required]
        [EmailAddress]
        [DisplayName("E-mail")]
        public string Email { get; set; } = "";
        [Required]
        [Phone]
        public string Phone { get; set; } = "";
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";
        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
