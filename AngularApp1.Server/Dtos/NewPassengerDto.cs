using System.ComponentModel.DataAnnotations;

namespace AngularApp1.Server.Dtos
{
    public record NewPassengerDto(
        [Required] string Email,
        [Required] string FirstName,
        [Required] string LastName,
        [Required] bool gender);
    
}
 