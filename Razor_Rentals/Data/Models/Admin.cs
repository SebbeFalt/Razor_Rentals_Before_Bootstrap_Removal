using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Razor_Rentals.Data.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
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
        public List<Car> Cars { get; set; } = new List<Car>();
    }
}
