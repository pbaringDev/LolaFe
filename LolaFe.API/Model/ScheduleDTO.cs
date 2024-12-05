using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LolaFe.API.Model
{
    public class ScheduleDTO
    {

        public int Id { get; set; }

        public DateTime PickupDate { get; set; }
        public DateTime DeliveryDate { get; set; }

        public string TimeSlot { get; set; } // e.g., "10:00 AM - 12:00 PM"
    }
}
