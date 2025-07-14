using System.ComponentModel.DataAnnotations;

namespace AngularApp1.Server.Domain.Entities
{
    public record Passenger(
         string Email,
         string FirstName,
         string LastName,
         bool gender);
    
}
 