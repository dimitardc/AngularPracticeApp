

using System.ComponentModel.DataAnnotations;

namespace AngularApp1.Server.Dtos
{
    public record BookDto(
        [Required] Guid FlightId,
        [Required] string PassengerEmail,
        [Required] byte NumberOfSeats);
}
