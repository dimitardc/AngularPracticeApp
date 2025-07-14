using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AngularApp1.Server.ReadModels;
using AngularApp1.Server.Data;
using AngularApp1.Server.Dtos;
using AngularApp1.Server.Domain.Errors;


namespace AngularApp1.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly Entities _entities;

        public BookingController(Entities entities) {
            _entities = entities;
        }


        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(IEnumerable<BookingRm>), 200)]
        [HttpGet("{email}")]
        public ActionResult<IEnumerable<BookingRm>>  List(string email)
        {
            var bookings = _entities.Flights.ToArray()
                .SelectMany(f => f.bookings
                .Where(b => b.PassengerEmail == email)
                .Select(b=> new BookingRm(
                    f.Id,
                    f.Airline,
                    f.Price.ToString(),
                    new TimePlaceRm(f.Arrival.Place, f.Arrival.Time),
                    new TimePlaceRm(f.Departure.Place, f.Departure.Time),
                    b.NumberOfSeats,
                    email)));

            return Ok(bookings);
        }


        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
 
        public IActionResult Cancel(BookDto book)
        {
            var flight = _entities.Flights.SingleOrDefault(f => f.Id == book.FlightId);
            var error = flight?.CancelBooking(book.PassengerEmail, book.NumberOfSeats);
            if (error == null)
            {
                _entities.SaveChanges();
                return NoContent(); //success call for "ive deleted something"
            }
            
            if(error is NotFoundError)
            {
                return NotFound();
            }
            throw new Exception($"theres an error of type :{error.GetType().Name}");
        }
    }
}
