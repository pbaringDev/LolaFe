using LolaFe.API.Model;
using LolaFe.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace LolaFe.API.Controllers
{
    [Route("api/bookings/{bookingId}/schedules")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly ILogger<SchedulesController> _Logger;
        private readonly BookingDataStore _BookingDataStore;
        private readonly IMailService _MailService;

        public SchedulesController(ILogger<SchedulesController> logger, BookingDataStore bookingDataStore, IMailService mailService)
        {
            _Logger = logger ?? throw new ArgumentException(nameof(logger));
            _BookingDataStore = bookingDataStore ?? throw new ArgumentException(nameof(bookingDataStore));
            _MailService =  mailService ?? throw new ArgumentException(nameof(mailService));
        }

        [HttpGet(Name = "GetSchedule")]
        public ActionResult<ScheduleDTO> GetSchedules(int bookingId)
        {
            //1 find main
            var booking = _BookingDataStore.Bookings.FirstOrDefault(x => x.Id == bookingId);

            //2 validate main
            if (booking == null)
            {
                return NotFound();
            }

            //3 find sub
            var schedule = booking.Schedule;

            //4 validate sub
            if (schedule == null)
            {

                return NotFound();
            }

            return Ok(booking.Schedule);        
        }

        //POST-CreateDTO
        [HttpPost]
        public ActionResult<ScheduleDTO> CreateSchedule(int bookingId, ScheduleCreateDTO schedule)
        {
            var booking = _BookingDataStore.Bookings.FirstOrDefault(x => x.Id == bookingId);

            if (booking == null)
            {
                return NotFound();
            }

            //3 find sub
            var prevSchedule = booking.Schedule;

            //4 should have empty schedule
            if (prevSchedule != null)
            {
                return BadRequest();
            }

            //TODO create incremental id
            var scheduleId = _BookingDataStore.Bookings.Select(b => b.Schedule).Max(s => s.Id); // get max id for now and increment by 1
            var newSchedule = new ScheduleDTO
            {

                Id = ++scheduleId,
                DeliveryDate = schedule.DeliveryDate,
                PickupDate = schedule.PickupDate,
                TimeSlot = schedule.TimeSlot
            };


            //5 update
            booking.Schedule = newSchedule;

            return CreatedAtRoute("GetSchedule", new { bookingId }, newSchedule);


        }
        //PUT UpdateDTO
        [HttpPut]
        public ActionResult UpdateSchedule(int bookingId, ScheduleUpdateDTO schedule)
        {
            //1 get main entity => booking
            var booking = _BookingDataStore.Bookings.FirstOrDefault(x => x.Id == bookingId);

            //2 validate
            if (booking == null)
            {
                return NotFound();
            }

            //3 find sub
            var prevSchedule = booking.Schedule;

            //4 should have schedule
            if (prevSchedule == null)
            {
                return NotFound();
            }

            //5 update
            prevSchedule.TimeSlot = schedule.TimeSlot;
            prevSchedule.PickupDate = schedule.PickupDate;
            prevSchedule.DeliveryDate = schedule.DeliveryDate;

            //successful but no returned object
            return NoContent();

        }

        //PATCH JSONPATCH
        /// <summary>
        /// accept JsonPatchDocument
        /// e.g  
        /// {
        ///   "op": "replace",
        ///   "path": "/timeSlot",
        ///   "value": "09:00 AM - 12:00 PM"
        /// }
        /// op == add,remove,replace,move,copy,test
        /// </summary>
        /// <param name="bookingId"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        [HttpPatch]
        public ActionResult PartialUpdateSchedule(int bookingId, JsonPatchDocument<ScheduleUpdateDTO> patchDocument)
        {
            //1 get main entity => booking
            var booking = _BookingDataStore.Bookings.FirstOrDefault(x => x.Id == bookingId);

            //2 validate
            if (booking == null)
            {
                return NotFound();
            }

            //3 find sub
            var prevSchedule = booking.Schedule;

            //4 should have schedule
            if (prevSchedule == null)
            {
                return NotFound();
            }

            //5 apply patch

            var scheduleToPatch = new ScheduleUpdateDTO
            {

                TimeSlot = prevSchedule.TimeSlot,
                PickupDate = prevSchedule.PickupDate,
                DeliveryDate = prevSchedule.DeliveryDate
            };

            patchDocument.ApplyTo(scheduleToPatch, ModelState);

            //validate model state

            if (!ModelState.IsValid)
            {
                return BadRequest();

            }

            if (!TryValidateModel(scheduleToPatch))
            {
                return BadRequest();
            }

            // update
            prevSchedule.TimeSlot = scheduleToPatch.TimeSlot;
            prevSchedule.PickupDate = scheduleToPatch.PickupDate;
            prevSchedule.DeliveryDate = scheduleToPatch.DeliveryDate;

            return NoContent();
        }

        //DELETE
        [HttpDelete]
        public ActionResult DeleteSchedule(int bookingId)
        {
            //1 get main entity => booking
            var booking = _BookingDataStore.Bookings.FirstOrDefault(x => x.Id == bookingId);

            //2 validate
            if (booking == null)
            {
                return NotFound();
            }

            //remove
            booking.Schedule = null;

            var message = $"Removed: schedule with id {bookingId}";
            _Logger.LogInformation(message);
            _MailService.Send("Delete Schedule", message);
            return NoContent();
        }


    }
}
