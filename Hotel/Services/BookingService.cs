using Hotel.Data;
using Hotel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services
{
    public class BookingService
    {
        ApplicationDbContext _dbContext;


        public BookingService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateBooking(Booking booking)
        {

            _dbContext.Bookings.Add(booking);
            _dbContext.SaveChanges();
        }


        public Booking GetBooking(int bookingId)
        {
            return null;
        }


        public List<Guest> GetAllBookings()
        {
            return null;
        }


        public string UpdateBooking(int bookingId, Booking booking)
        {
            return "Return status message (success or failure)";
        }

        public string DeleteBooking(Booking booking)
        {
            return "Return status message (success or failure)";
        }

        public string PayBooking(Booking booking)
        {
            return "Return status message (success or failure)";
        }


        public List<Booking> GetBookings()
        {
            return _dbContext.Bookings.ToList();
        }
    }
}
