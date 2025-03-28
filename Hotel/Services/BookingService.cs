using Hotel.Data;
using Hotel.Entities;
using Microsoft.EntityFrameworkCore;
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
            return _dbContext.Bookings
                .Include(b => b.Room)
                .Include(b => b.Guest)
                .FirstOrDefault(b => b.BookingId == bookingId);
        }

        public List<Guest> AllBookings => _dbContext.Guests.ToList();

        public string UpdateBooking(int bookingId, Booking booking)
        {
            var existingBooking = _dbContext.Bookings.Find(bookingId);
            if (existingBooking == null)
            {
                return "Booking not found";
            }

            existingBooking.CheckIn = booking.CheckIn;
            existingBooking.CheckOut = booking.CheckOut;
            existingBooking.NumberOfGuests = booking.NumberOfGuests;
            existingBooking.IsPaid = booking.IsPaid;
            existingBooking.Room = booking.Room;
            existingBooking.Guest = booking.Guest;

            _dbContext.Bookings.Update(existingBooking);
            _dbContext.SaveChanges();

            return "Booking updated successfully";
        }

        public string DeleteBooking(Booking booking)
        {
            _dbContext.Bookings.Remove(booking);
            _dbContext.SaveChanges();
            return "Booking deleted successfully";
        }

        public string PayBooking(Booking booking)
        {
            var existingBooking = _dbContext.Bookings.Find(booking.BookingId);
            if (existingBooking == null)
            {
                return "Booking not found";
            }

            existingBooking.IsPaid = true;
            _dbContext.Bookings.Update(existingBooking);
            _dbContext.SaveChanges();

            return "Booking paid successfully";
        }

        public List<Booking> GetBookings()
        {
            return _dbContext.Bookings
                .Include(b => b.Room)
                .Include(b => b.Guest)
                .ToList();
        }
    }
}
