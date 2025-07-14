using AngularApp1.Server.ReadModels;
using Microsoft.AspNetCore.Mvc;
using AngularApp1.Server.Dtos;
using AngularApp1.Server.Domain.Entities;
using AngularApp1.Server.Domain.Errors;
using AngularApp1.Server.Data;
using Microsoft.EntityFrameworkCore;
namespace AngularApp1.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightController : ControllerBase
    {
        
        private readonly ILogger<FlightController> _logger;

        public readonly Entities _entities;


        public FlightController(ILogger<FlightController> logger, Entities entities)
        {
            _logger = logger;
            _entities = entities;
        }


        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(IEnumerable<FlightRm>), 200)]
        [HttpGet]
        public IEnumerable<FlightRm> Search([FromQuery] FlightSearchParametersDto flightSearchParametersDto)
        {
            IQueryable<Flight> flights = _entities.Flights;

            if(!string.IsNullOrEmpty(flightSearchParametersDto.destination))
                flights = flights.Where(f => f.Arrival.Place.Contains(flightSearchParametersDto.destination));

            var flightRmList = flights
                .Select(flight => new FlightRm(
                flight.Id,
                flight.Airline,
                flight.Price,
                new TimePlaceRm(flight.Departure.Place.ToString(), flight.Departure.Time),
                new TimePlaceRm(flight.Arrival.Place.ToString(), flight.Arrival.Time),
                flight.RemainingNumberOfSeats
            )).ToArray();
            return flightRmList;
        }



        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(FlightRm), 200)]
        [HttpGet("{id}")]
        public ActionResult<FlightRm> Find(Guid id)
        {
            var flight = _entities.Flights.SingleOrDefault(f => f.Id == id);

            if (flight == null) {
                return NotFound();
            }

            var flightRm = new FlightRm(
                flight.Id,
                flight.Airline,
                flight.Price, 
                new TimePlaceRm(flight.Departure.Place.ToString(), flight.Departure.Time),
                new TimePlaceRm(flight.Arrival.Place.ToString(), flight.Arrival.Time), 
                flight.RemainingNumberOfSeats
            );

            return Ok(flightRm);
        }



        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public IActionResult Book(BookDto bookDto)
        {
            System.Diagnostics.Debug.WriteLine($"Booking a new flight {bookDto.FlightId}");
            var flight = _entities.Flights.SingleOrDefault(f => f.Id == bookDto.FlightId);
            if (flight == null) {
                return NotFound();
            }

            //can use MakeBooking or just leave it like this
            /*if(flight.RemainingNumberOfSeats < bookDto.NumberOfSeats)
            {
                return Conflict(new { message = "not enough seats" });
            }

            
            Booking booking = new Booking( bookDto.PassengerEmail, bookDto.NumberOfSeats)
            flight.bookings.Add(booking);

            flight.RemainingNumberOfSeats -= bookDto.NumberOfSeats;*/
            var error = flight.MakeBooking(bookDto.PassengerEmail, bookDto.NumberOfSeats);
            if (error is OverbookError)
            {
            }

            try
            {
                _entities.SaveChanges();

            }
            catch(DbUpdateConcurrencyException e)
            {
                return Conflict(new { message = "An error occured while booking" });

            }

            return CreatedAtAction(nameof(Find), new {id = bookDto.FlightId}, bookDto);
        }
    }
}
