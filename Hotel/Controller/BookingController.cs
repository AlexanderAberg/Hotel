using Hotel.Data;
using Hotel.Entities;
using Hotel.Menus;
using Hotel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Controller
{
    public class BookingController
    {
        private BookingService _bookingService;
        private RoomService _roomService;
        private GuestService _guestService;

        public BookingController(BookingService bookingService, RoomService roomService, GuestService guestService)
        {
            _bookingService = bookingService;
            _roomService = roomService;
            _guestService = guestService;
            var bookings = _bookingService.GetBookings();
            Console.WriteLine($"Number of bookings retrieved: {bookings?.Count ?? 0}");
        }

        public BookingController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public void CreateBooking()
        {
            Console.WriteLine("Skriv in bokningsdetaljer");
            Console.WriteLine("Skriv in rumsnummer");
            string roomNumber = Console.ReadLine();
            Console.WriteLine("Skriv in incheckningsdatumet");
            DateTime checkInDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Skriv in utcheckningdatumet");
            DateTime checkOutDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Skriv antalet gäster");
            int numberOfGuests = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Skriv in betalningsstatus (true/false)");
            bool isPaid = Convert.ToBoolean(Console.ReadLine());
            Console.WriteLine("Skriv in gästId");
            int guestId = Convert.ToInt32(Console.ReadLine());

            Room room = _roomService.GetRoom(roomNumber);
            Guest guest = _guestService.GetGuest(guestId);

            if (room == null)
            {
                Console.WriteLine("Rum finns inte.");
                return;
            }

            if (guest == null)
            {
                Console.WriteLine("Kan inte hitta gäst.");
                return;
            }

            Booking booking = new Booking()
            {
                Room = room,
                CheckIn = checkInDate,
                CheckOut = checkOutDate,
                NumberOfGuests = numberOfGuests,
                IsPaid = isPaid,
                Guest = guest
            };

            _bookingService.CreateBooking(booking);
            Console.WriteLine("Bokningen har skapats och kommer ha detta bokningsId: " + booking.BookingId);
        }

        public void UpdateBooking()
        {
            Console.WriteLine("Enter Booking Id");
            int bookingId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Room Number");
            string roomNumber = Console.ReadLine();
            Console.WriteLine("Enter Check In Date");
            DateTime checkInDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter Check Out Date");
            DateTime checkOutDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter Number of Guests");
            int numberOfGuests = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Payment Status (true/false)");
            bool isPaid = Convert.ToBoolean(Console.ReadLine());
            Console.WriteLine("Enter Guest Id");
            int guestId = Convert.ToInt32(Console.ReadLine());
            Room room = _roomService.GetRoom(roomNumber);
            Guest guest = _guestService.GetGuest(guestId);
            if (room == null)
            {
                Console.WriteLine("Rummet existerar inte");
                return;
            }
            if (guest == null)
            {
                Console.WriteLine("Kan inte hitta gäst.");
                return;
            }
            Booking booking = new Booking()
            {
                Room = room,
                CheckIn = checkInDate,
                CheckOut = checkOutDate,
                NumberOfGuests = numberOfGuests,
                IsPaid = isPaid,
                Guest = guest
            };
            _bookingService.UpdateBooking(bookingId, booking);
            Console.WriteLine("Bokningen har uppdaterats.");
        }

        public void DeleteBooking()
        {
            Console.WriteLine("Skriv in bokningsId");
            int bookingId = Convert.ToInt32(Console.ReadLine());
            Booking booking = _bookingService.GetBooking(bookingId);
            if (booking == null)
            {
                Console.WriteLine("Bokningen kan inte hittas.");
                return;
            }
            _bookingService.DeleteBooking(booking);
            Console.WriteLine("Bokningen har tagit bort.");
        }

        public void ListBookings()
        {
            if (_bookingService == null)
            {
                Console.WriteLine("Kan inte hitta några bokningar");
                return;
            }

            var bookings = _bookingService.GetBookings();

            if (bookings == null || bookings.Count == 0)
            {
                Console.WriteLine("Inga bokningar kan hittas");
                return;
            }

            // Skriv ut rubrikerna först
            Console.WriteLine(
                "Booking Id\tRoom Number\tCheck In\tCheck Out\tNumber of Guests\tIs Paid\tGuest Name");

            // Skriv ut varje bokning med alla relationer
            foreach (Booking booking in bookings)
            {
                Console.WriteLine(
                    $"{booking.BookingId}\t{booking.Room?.RoomNumber}\t{booking.CheckIn}\t{booking.CheckOut}\t{booking.NumberOfGuests}\t{booking.IsPaid}\t{booking.Guest?.FirstName + booking.Guest?.LastName}");
            }

            Console.WriteLine("Tryck på valfri tangent för att återgå till menyn");
            Console.ReadKey();
        }

        public void PayBooking()
        {
            Console.WriteLine("Skriv in bokningsId");
            int bookingId = Convert.ToInt32(Console.ReadLine());
            Booking booking = _bookingService.GetBooking(bookingId);
            if (booking == null)
            {
                Console.WriteLine("Kan inte hitta bokningen");
                return;
            }
            _bookingService.PayBooking(booking);
            Console.WriteLine("Bokningens betalning har gått igenom.");
        }
    }
}