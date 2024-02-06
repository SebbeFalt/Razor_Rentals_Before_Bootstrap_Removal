using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Razor_Rentals.Data.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Description { get; set; } = "";
        [Required]
        public bool Available { get; set; }
        [Required]
        [DisplayName("Cost / Day (SEK)")]
        public decimal CostPerDay { get; set; }
        [Required]
        [DisplayName("Image")]
        public string ImageURL { get; set; } = "";
        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
