using AngularApp1.Server.Domain.Errors;
using AngularApp1.Server.Dtos;
using AngularApp1.Server.ReadModels;

namespace AngularApp1.Server.Domain.Entities
{
    public class Flight
    {

        public Guid Id { get; set; }
        public string Airline { get; set; }
        public string Price { get; set; }
        public TimePlace Departure { get; set; }
        public TimePlace Arrival { get; set; }
        public int RemainingNumberOfSeats { get; set; }

        public IList<Booking> bookings = new List<Booking>();

        public Flight()
        {
            
        }

        public Flight(Guid id,string airline,string price,TimePlace departure,TimePlace arrival,int remainingNumberOfSeats)
        {
            Id = id;
            Airline = airline;
            Price = price;
            Departure = departure;
            Arrival = arrival;
            RemainingNumberOfSeats = remainingNumberOfSeats;
            
        }

        internal object? MakeBooking(string passengerEmail, byte numberOfSeats)
        {
            var flight = this;
            if (flight.RemainingNumberOfSeats < numberOfSeats)
            {
                return new OverbookError();
            }


            Booking booking = new Booking(passengerEmail, numberOfSeats);
            flight.bookings.Add(booking);

            flight.RemainingNumberOfSeats -= numberOfSeats;
            return null;
        }

        public object? CancelBooking(string passengerEmail, byte numberOfSeats)
        {
            var booking = bookings.FirstOrDefault(b=> b.PassengerEmail.ToLower() == passengerEmail.ToLower() 
            && b.NumberOfSeats == numberOfSeats);
            if(booking == null)
            {
                return new NotFoundError();
            }

            bookings.Remove(booking);
            RemainingNumberOfSeats += numberOfSeats;

            return null;
        }
    }
}
