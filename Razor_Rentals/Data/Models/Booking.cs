using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Razor_Rentals.Data.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        [Required]
        public Customer Customer { get; set; } = null!;
        [Required]
        public Car Car { get; set; } = null!;
        [Required]
        [DisplayName("Booking Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime BookingDate { get; set; }
        [Required]
        [DisplayName("Pickup Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime CarPickupDate { get; set; }
        [Required]
        [DisplayName("Return Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime CarReturnDate { get; set; }
        [DisplayName("Cost (SEK)")]
        public decimal Cost { get; set; }
        public Booking()
        {
            BookingDate = DateTime.UtcNow.Date;
        }
    }
}
