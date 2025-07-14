namespace AngularApp1.Server.ReadModels
{
    public record BookingRm(
        Guid Id, 
        string Airline, 
        string Price, 
        TimePlaceRm Arrival, 
        TimePlaceRm Departure, 
        int NumberOfSeats, 
        string PassengerEmail);
}
