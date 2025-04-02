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
        private ApplicationDbContext _dbContext;

        public BookingService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateBooking(Booking booking)
        {
            if (booking == null)
                throw new ArgumentNullException(nameof(booking));

            if (booking.CheckOut <= booking.CheckIn)
                throw new ArgumentException("Utcheckningsdatumet måste vara efter incheckningsdatumet");

            if (booking.NumberOfGuests < 1)
                throw new ArgumentException("Antal gäster måste vara minst 1");

            if ((booking.CheckIn - DateTime.Now).TotalDays <= 10)
                booking.IsPaid = true;
            else
                booking.IsPaid = false;
            if ((booking.CheckIn - DateTime.Now).TotalDays < 10)
                booking.IsPaid = true;
            else
                throw new ArgumentException("Bokningen måste betalas direkt, eftersom det är färre än 10 dagar till vistelse");
            

            _dbContext.Bookings.Add(booking);
            _dbContext.SaveChanges();
            string successMessage = $"Bokning med ID {booking.BookingId} skapades framgångsrikt.";
            Console.WriteLine(successMessage);
            Console.ReadLine();
        }


        public Booking? GetBooking(int bookingId)
        {
            return _dbContext.Bookings
                .Include(b => b.Room)
                .Include(b => b.Guest)
                .FirstOrDefault(b => b.BookingId == bookingId);
        }

        public string UpdateBooking(int bookingId, Booking booking)
        {
            var existingBooking = _dbContext.Bookings.Find(bookingId);
            if (existingBooking == null)
            {
                return "Kan inte hitta bokningen";
            }

            existingBooking.CheckIn = booking.CheckIn;
            existingBooking.CheckOut = booking.CheckOut;
            existingBooking.NumberOfGuests = booking.NumberOfGuests;
            existingBooking.IsPaid = booking.IsPaid;
            existingBooking.Room = booking.Room;
            existingBooking.Guest = booking.Guest;

            _dbContext.Bookings.Update(existingBooking);
            _dbContext.SaveChanges();

            return "Bokningen uppdaterades";
        }

        public string DeleteBooking(Booking booking)
        {
            _dbContext.Bookings.Remove(booking);
            _dbContext.SaveChanges();
            return "Bokningen togs bort";
        }

        public string PayBooking(Booking booking)
        {
            var existingBooking = _dbContext.Bookings.Find(booking.BookingId);
            if (existingBooking == null)
            {
                return "Kan inte hitta bokningen";
            }

            existingBooking.IsPaid = true;
            _dbContext.Bookings.Update(existingBooking);
            _dbContext.SaveChanges();

            return "Bokningen betalades";
        }

        public List<Booking> GetBookings()
        {
            return _dbContext.Bookings
                .Include(b => b.Room)
                .Include(b => b.Guest)
                .ToList();
        }

        public bool IsRoomAvailable(Room room, DateTime checkIn, DateTime checkOut)
        {
            return _dbContext.Bookings
                .Where(b => b.RoomNumber == room.RoomNumber)
                .All(b => b.CheckOut <= checkIn || b.CheckIn >= checkOut);
        }
    }
}