using LolaFe.API.Model;
using Microsoft.AspNetCore.Mvc;

namespace LolaFe.API.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingsController : ControllerBase
    {
        private readonly BookingDataStore _BookingDataStore;

        public BookingsController(BookingDataStore bookingDataStore) 
        {
            _BookingDataStore = bookingDataStore ?? throw new ArgumentException(nameof(bookingDataStore));        
        }


        [HttpGet]
        public ActionResult<List<BookingDTO>> GetBookings()
        {
            var bookings = _BookingDataStore.Bookings;

            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public ActionResult GetBooking(int id) 
        {
            var booking = _BookingDataStore.Bookings.SingleOrDefault(x => x.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }
    }
}
