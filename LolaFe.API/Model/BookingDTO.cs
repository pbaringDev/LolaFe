namespace LolaFe.API.Model
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string UserName { get; set; }

        public string ServiceType { get; set; } //"Wash", "Dry", "Iron"
        public double Price { get; set; }

        public string Status { get; set; } //"Pending", "In Progress", "Completed"

        public ScheduleDTO? Schedule { get; set; }
    }
}
