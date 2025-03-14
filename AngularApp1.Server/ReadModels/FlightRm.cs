namespace AngularApp1.Server.ReadModels
{
    public record FlightRm(
        Guid id,
        string Airline,
        string Price,
        TimePlaceRm departure,
        TimePlaceRm Arrival,
        int RemainingNumberOfSeats
        );
}
