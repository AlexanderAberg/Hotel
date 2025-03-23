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
            Console.WriteLine("Enter Booking Details");
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
                Console.WriteLine("Room not found.");
                return;
            }

            if (guest == null)
            {
                Console.WriteLine("Guest not found.");
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
            Console.WriteLine("Booking created and will now have Booking Id: " + booking.BookingId);
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
                Console.WriteLine("Room not found.");
                return;
            }
            if (guest == null)
            {
                Console.WriteLine("Guest not found.");
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
            Console.WriteLine("Booking updated successfully.");
        }

        public void DeleteBooking()
        {
            Console.WriteLine("Enter Booking Id");
            int bookingId = Convert.ToInt32(Console.ReadLine());
            Booking booking = _bookingService.GetBooking(bookingId);
            if (booking == null)
            {
                Console.WriteLine("Booking not found.");
                return;
            }
            _bookingService.DeleteBooking(booking);
            Console.WriteLine("Booking deleted successfully.");
        }

        public void ListBookings()
        {
            Console.WriteLine(
                "Booking Id\tRoom Number\tCheck In\tCheck Out\tNumber of Guests\tIs Paid\tGuest Id");
            foreach (Booking booking in _bookingService.GetBookings())
            {
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
            Console.WriteLine("Enter Booking Id");
            int bookingId = Convert.ToInt32(Console.ReadLine());
            Booking booking = _bookingService.GetBooking(bookingId);
            if (booking == null)
            {
                Console.WriteLine("Booking not found.");
                return;
            }
            _bookingService.PayBooking(booking);
            Console.WriteLine("Booking paid successfully.");
        }
    }
}
