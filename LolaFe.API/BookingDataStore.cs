using LolaFe.API.Model;

namespace LolaFe.API
{
    public class BookingDataStore
    {
        public List<BookingDTO> Bookings { get; set; }

        //singleton
        //public static BookingDataStore Current { get;} = new BookingDataStore();

        public BookingDataStore()
        {
            //init data

            Bookings = new List<BookingDTO>
            {
                new BookingDTO {
                        Id = 1,
                        CreatedAt = DateTime.Now.AddHours(-1),
                        UpdatedAt = DateTime.Now,
                        ServiceType = "Wash",
                        Price = 50,
                        Status = "Pending",
                        UserName = "Pinky",
                        Schedule = new ScheduleDTO
                            {
                                 Id = 1,
                                 DeliveryDate = DateTime.Now,
                                 PickupDate = DateTime.Now.AddMinutes(-30),
                                 TimeSlot = "10:00 AM - 12:00 PM"
                            }
                },

                 new BookingDTO {
                        Id = 2,
                        CreatedAt = DateTime.Now.AddHours(-2),
                        UpdatedAt = DateTime.Now,
                        ServiceType = "Dry",
                        Price = 50,
                        Status = "In Progress",
                        UserName = "Cleofe",
                        Schedule = new ScheduleDTO
                            {
                                 Id = 2,
                                 DeliveryDate = DateTime.Now,
                                 PickupDate = DateTime.Now.AddMinutes(-90),
                                 TimeSlot = "09:00 AM - 11:00 AM"
                            }
                },
                 new BookingDTO {
                        Id = 3,
                        CreatedAt = DateTime.Now.AddHours(-3),
                        UpdatedAt = DateTime.Now,
                        ServiceType = "Iron",
                        Price = 50,
                        Status = "Completed",
                        UserName = "Paul",
                        Schedule = new ScheduleDTO
                            {
                                 Id = 2,
                                 DeliveryDate = DateTime.Now,
                                 PickupDate = DateTime.Now.AddHours(-2),
                                 TimeSlot = "08:00 AM - 10:00 AM"
                            }
                }

             };
        }
    }
}
