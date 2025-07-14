using AngularApp1.Server.Domain.Entities;
using AngularApp1.Server.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace AngularApp1.Server.Data
{
    public class Entities : DbContext
    {
        public DbSet<Passenger> Passengers => Set<Passenger>();
        public DbSet<Flight> Flights => Set<Flight>();


        public Entities(DbContextOptions<Entities> options) : base(options)
        {
        }


        //ADDREF
        //OnModelCreating is a method that lets you fine-tune
        //how your domain models are mapped to the database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Passenger>().HasKey(p => p.Email);

            modelBuilder.Entity<Flight>().Property(p => p.RemainingNumberOfSeats).IsConcurrencyToken();

            modelBuilder.Entity<Flight>().OwnsOne(f => f.Departure);
            modelBuilder.Entity<Flight>().OwnsOne(f => f.Arrival);
            modelBuilder.Entity<Flight>().OwnsMany(f => f.bookings);

        }
    }
}
