using System.ComponentModel;

namespace AngularApp1.Server.Dtos
{
    public record FlightSearchParametersDto(
        [DefaultValue ("12/25/2025 10:30:00 AM")]
        DateTime? fromDate,

        [DefaultValue ("12/25/2025 10:30:00 AM")]   
        DateTime? toDate,

        [DefaultValue("Los Angeles")]
        string? from,

        [DefaultValue("Berlin")]
        string? destination,

        [DefaultValue(1)]
        int? numberOfPassengers
        );
}
