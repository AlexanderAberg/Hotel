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
        private BookingService bookingService;

        public BookingController(BookingService bookingService, RoomService roomService)
        {
            _bookingService = bookingService;
            _roomService = roomService;
        }

        public BookingController(BookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        public void CreateBooking()
        {
            Console.WriteLine("Skriv in bokningsdetaljer");
            Console.WriteLine("Skriv in rumsnummer");
            int roomNumber = Convert.ToInt32(Console.ReadLine());
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
                Rooms = new List<Room> { room },
                CheckIn = checkInDate,
                CheckOut = checkOutDate,
                NumberOfGuests = numberOfGuests,
                IsPaid = isPaid,
                Guests = new List<Guest> { guest }
            };

            _bookingService.CreateBooking(booking);
            Console.WriteLine("Bokningen har skapats och kommer ha detta bokningsId: " + booking.BookingId);
        }

        public void UpdateBooking()
        {
            Console.WriteLine("Enter Booking Id");
            int bookingId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Room Number");
            int roomNumber = Convert.ToInt32(Console.ReadLine());
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
                BookingId = bookingId,
                Rooms = new List<Room> { room },
                CheckIn = checkInDate,
                CheckOut = checkOutDate,
                NumberOfGuests = numberOfGuests,
                IsPaid = isPaid,
                Guests = new List<Guest> { guest }
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
            if (bookings == null)
            {
                Console.WriteLine("Inga bokningar kan hittas");
                return;
            }

            Console.WriteLine(
                "Booking Id\tRoom Number\tCheck In\tCheck Out\tNumber of Guests\tIs Paid\tGuest Id");
            foreach (Booking booking in bookings)
            {
                if (booking.Rooms == null || booking.Rooms.Count == 0 || booking.Guests == null || booking.Guests.Count == 0)
                {
                    Console.WriteLine($"Bokningen {booking.BookingId} har ej all data.");
                    continue;
                }

                Console.WriteLine(
                    booking.BookingId + "\t" +
                    booking.Rooms[0].RoomNumber + "\t" +
                    booking.CheckIn + "\t" +
                    booking.CheckOut + "\t" +
                    booking.NumberOfGuests + "\t" +
                    booking.IsPaid + "\t" +
                    booking.Guests[0].GuestId);
            }
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
