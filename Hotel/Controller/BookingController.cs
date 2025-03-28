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
        private readonly RoomService _roomService;
        private readonly GuestService _guestService;
        private readonly BookingService _bookingService;

        public BookingController(RoomService roomService, GuestService guestService, BookingService bookingService)
        {
            _roomService = roomService ?? throw new ArgumentNullException(nameof(roomService));
            _guestService = guestService ?? throw new ArgumentNullException(nameof(guestService));
            _bookingService = bookingService ?? throw new ArgumentNullException(nameof(bookingService));
        }

        public BookingController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public void CreateBooking()
        {
            Console.WriteLine("Skriv in bokningsdetaljer");

            string roomNumber = GetValidRoomNumber();
            if (string.IsNullOrEmpty(roomNumber))
                return;

            DateTime checkInDate = GetValidDate("Skriv in incheckningsdatumet (ÅÅÅÅ-MM-DD): ");
            if (checkInDate == DateTime.MinValue)
                return;

            DateTime checkOutDate = GetValidDate("Skriv in utcheckningsdatumet (ÅÅÅÅ-MM-DD): ");
            if (checkOutDate == DateTime.MinValue)
                return;

            int numberOfGuests = GetValidNumberOfGuests();
            if (numberOfGuests == 0)
                return;

            int guestId = GetValidGuestId();
            if (guestId == 0)
                return;

            Room room = _roomService.GetRoom(roomNumber);
            Guest guest = _guestService.GetGuest(guestId);

            if (room == null)
            {
                Console.WriteLine("Rummet finns inte.");
                return;
            }

            if (guest == null)
            {
                Console.WriteLine("Kan inte hitta gäst.");
                return;
            }

            if (!_bookingService.IsRoomAvailable(room, checkInDate, checkOutDate))
            {
                Console.WriteLine("Rummet är inte tillgängligt under den valda perioden.");
                return;
            }

            Booking booking = new Booking()
            {
                Room = room,
                CheckIn = checkInDate,
                CheckOut = checkOutDate,
                NumberOfGuests = numberOfGuests,
                IsPaid = false,
                Guest = guest
            };

            try
            {
                _bookingService.CreateBooking(booking);
                Console.WriteLine($"Bokningen har skapats och kommer ha detta bokningsId: {booking.BookingId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel uppstod när bokningen skulle skapas: {ex.Message}");
            }
        }

        public void UpdateBooking()
        {
            Console.WriteLine("Skriv in bokningsId");
            int bookingId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Skriv in rumsnummer");
            string roomNumber = Console.ReadLine();
            Console.WriteLine("Skriv in incheckningsdatum");
            DateTime checkInDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Skriv in utcheckningsdatum");
            DateTime checkOutDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Skriv in antalet gäster");
            int numberOfGuests = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Skriv in betalningsstatus (true/false)");
            bool isPaid = Convert.ToBoolean(Console.ReadLine());
            Console.WriteLine("Skriv in gästId");
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
            Console.WriteLine(
                "Booking Id\tRoom Number\tCheck In\tCheck Out\tNumber of Guests\tIs Paid\tGuest Name");

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

        private string GetValidRoomNumber()
        {
            while (true)
            {
                Console.Write("Skriv in rumsnummer: ");
                string roomNumber = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(roomNumber))
                    return roomNumber;

                Console.WriteLine("Rumsnummer kan inte vara tomt. Försök igen.");
            }
        }

        private DateTime GetValidDate(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string dateInput = Console.ReadLine();

                if (DateTime.TryParse(dateInput, out DateTime date))
                {
                    if (date >= DateTime.Now)
                        return date;
                    else
                        Console.WriteLine("Datumet måste vara i framtiden.");
                }
                else
                    Console.WriteLine("Ogiltigt datum. Använd formatet ÅÅÅÅ-MM-DD.");
            }
        }

        private int GetValidNumberOfGuests()
        {
            while (true)
            {
                Console.Write("Skriv antalet gäster: ");
                if (int.TryParse(Console.ReadLine(), out int guests) && guests > 0)
                    return guests;

                Console.WriteLine("Ogiltigt antal gäster. Ange ett positivt heltal.");
            }
        }

        private int GetValidGuestId()
        {
            while (true)
            {
                Console.Write("Skriv in gästId: ");
                if (int.TryParse(Console.ReadLine(), out int guestId) && guestId > 0)
                    return guestId;

                Console.WriteLine("Ogiltigt gästID. Ange ett positivt heltal.");
            }
        }
    }
}