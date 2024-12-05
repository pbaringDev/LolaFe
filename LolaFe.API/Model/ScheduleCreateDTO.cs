using System.ComponentModel.DataAnnotations;

namespace LolaFe.API.Model
{
    public class ScheduleCreateDTO
    {
        //[Required()]
        public DateTime PickupDate { get; set; }


        [Required]
        public DateTime DeliveryDate { get; set; }


        [Required(ErrorMessage = "Timeslot is required. Must have start and end time")]
        [MaxLength(25)]
        public string TimeSlot { get; set; } = String.Empty; // e.g., "10:00 AM - 12:00 PM"
    }
}
