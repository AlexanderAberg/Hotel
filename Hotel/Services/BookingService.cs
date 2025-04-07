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
    public class BookingService(ApplicationDbContext dbContext)
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public void CreateBooking(Booking booking)
        {
            ArgumentNullException.ThrowIfNull(booking);

            if (booking.CheckOut <= booking.CheckIn)
                throw new ArgumentException("Utcheckningsdatumet måste vara efter incheckningsdatumet");

            if (booking.NumberOfGuests < 1)
                throw new ArgumentException("Antal gäster måste vara minst 1");

            booking.PaymentDueDate = booking.CheckIn.AddDays(-10);

            Console.WriteLine($"Incheckningsdatum: {booking.CheckIn:yyyy-MM-dd}");
            Console.WriteLine($": {booking.PaymentDueDate:yyyy-MM-dd} (10 dagarar före incheckning)");

            if ((booking.CheckIn - DateTime.Now).TotalDays <= 10)
            {
                booking.IsPaid = true;
                Console.WriteLine("Incheckning inom 10 dagar - ,måste betalas direkt");
            }
            else
            {
                booking.IsPaid = false;
            }

            _dbContext.Bookings.Add(booking);
            _dbContext.SaveChanges();

            Console.WriteLine($"Bokning skapad: ID={booking.BookingId}, Är betald?={booking.IsPaid}, Sista betalningsdag={booking.PaymentDueDate:yyyy-MM-dd}");
            Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
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

            if (existingBooking.IsPaid)
            {
                return "Bokningen är redan betald";
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
            if (checkIn >= checkOut)
            {
                throw new ArgumentException("Incheckningsdatum måste vara innan utcheckningdatumet.");
            }

            return _dbContext.Bookings
                .Where(b => b.RoomNumber == room.RoomNumber)
                .All(b => b.CheckOut <= checkIn || b.CheckIn >= checkOut);
        }
        public void RemoveUnpaidBookings()
        {
            var bookingsToFix = _dbContext.Bookings
                .Where(b => b.PaymentDueDate > b.CheckIn)
                .ToList();

            var allBookings = _dbContext.Bookings.ToList();
            Console.WriteLine($"Totalt antal bokningar: {allBookings.Count}");

            foreach (var booking in allBookings)
            {
                bool isPastDue = booking.PaymentDueDate.Date < DateTime.Now.Date;
                bool isUnpaid = booking.IsPaid == false;

                Console.WriteLine($"Bokning {booking.BookingId}: " +
                    $"Incheckning={booking.CheckIn:yyyy-MM-dd}, " +
                    $"Är betald?={booking.IsPaid}, " +
                    $"Sista inbetalningsdag={booking.PaymentDueDate:yyyy-MM-dd}, " +
                    $"Nu={DateTime.Now:yyyy-MM-dd}, " +
                    $"Har passerat sista inbetalningsdag={isPastDue}, " +
                    $"Är obetald={isUnpaid}");
            }

            var unpaidBookings = _dbContext.Bookings
                .AsEnumerable()
                .Where(b => b.IsPaid == false && b.PaymentDueDate.Date < DateTime.Now.Date)
                .ToList();

            Console.WriteLine($"Hittade {unpaidBookings.Count} obetalade bokningar att ta bort.");

            foreach (var booking in unpaidBookings)
            {
                Console.WriteLine($"Att ta bort: Booking ID: {booking.BookingId}, " +
                    $"Sista inbetalningsdag: {booking.PaymentDueDate:yyyy-MM-dd}, " +
                    $"Är betald?: {booking.IsPaid}");
            }

            if (unpaidBookings.Any())
            {
                try
                {
                    foreach (var booking in unpaidBookings)
                    {
                        _dbContext.Bookings.Remove(booking);
                    }
                    _dbContext.SaveChanges();
                    Console.WriteLine("Obetalda bokningar borttagna.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fel på borttagning av bokning: {ex.Message}");
                    if (ex.InnerException != null)
                        Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }
            else
            {
                Console.WriteLine("Inga obetalda bokningar att ta bort.");
            }

            Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
        }
    }
}