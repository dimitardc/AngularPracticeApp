using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AngularApp1.Server.Dtos;
using AngularApp1.Server.ReadModels;
using AngularApp1.Server.Domain.Entities;
using AngularApp1.Server.Data;

namespace AngularApp1.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {

        public readonly Entities _entities;

        public PassengerController(Entities entities)
        {
            _entities = entities;
        }


        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Register(NewPassengerDto dto)
        {
            Passenger p = new Passenger(dto.LastName, dto.FirstName, dto.Email, dto.gender);
            _entities.Passengers.Add(p);
            _entities.SaveChanges();
            
            return CreatedAtAction(nameof(Find), new { email = dto.Email}, dto);


        }

        [HttpGet("{email}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<PassengerRm> Find(string email)
        {
            var passenger = _entities.Passengers.FirstOrDefault(x => x.Email == email);
            if (passenger == null) { 
                return NotFound();
            }

            var rm = new PassengerRm(passenger.Email, passenger.FirstName, passenger.LastName, passenger.gender);
            return Ok(rm);

        } 

    }
}
