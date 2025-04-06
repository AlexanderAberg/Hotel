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
    public class BookingController(RoomService roomService, GuestService guestService, BookingService bookingService)
    {
        private readonly RoomService _roomService = roomService ?? throw new ArgumentNullException(nameof(roomService));
        private readonly GuestService _guestService = guestService ?? throw new ArgumentNullException(nameof(guestService));
        private readonly BookingService _bookingService = bookingService ?? throw new ArgumentNullException(nameof(bookingService));

        public void CreateBooking()
        {
            try
            {
                Console.WriteLine("Skriv in bokningsdetaljer");

                string roomNumber = GetValidRoomNumber(_roomService);
                if (string.IsNullOrEmpty(roomNumber))
                    return;

                DateTime checkInDate = GetValidDate("Skriv in incheckningsdatumet (ÅÅÅÅ-MM-DD): ");
                if (checkInDate == DateTime.MinValue)
                    return;

                DateTime checkOutDate = GetValidDate("Skriv in utcheckningsdatumet (ÅÅÅÅ-MM-DD): ");
                if (checkOutDate == DateTime.MinValue)
                    return;

                Room? room = _roomService.GetRoom(roomNumber);
                if (room == null)
                {
                    Console.WriteLine($"Rummet {roomNumber} finns inte.");
                    Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                    Console.ReadKey();
                    return;
                }

                int numberOfGuests = GetValidNumberOfGuests(room);
                if (numberOfGuests == 0)
                    return;

                int guestId = GetValidGuestId();
                if (guestId == 0)
                    return;

                Guest? guest = _guestService.GetGuest(guestId);
                if (guest == null)
                {
                    Console.WriteLine("Kan inte hitta gäst.");
                    Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                    Console.ReadKey();
                    return;
                }

                if (!_bookingService.IsRoomAvailable(room, checkInDate, checkOutDate))
                {
                    Console.WriteLine($"Rummet är inte tillgängligt mellan {checkInDate} och {checkOutDate}");
                    Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                    Console.ReadKey();
                    return;
                }

                bool isPaid = false;
                TimeSpan timeUntilCheckIn = checkInDate - DateTime.Now;
                if (timeUntilCheckIn.TotalDays <= 10)
                {
                    Console.WriteLine("Det är 10 eller färre dagar kvar till incheckning. Bokningen måste betalas nu.");
                    isPaid = true;
                }
                else
                {
                    Console.Write("Är bokningen betald? (true/false): ");
                    isPaid = Convert.ToBoolean(Console.ReadLine());
                }

                var booking = new Booking()
                {
                    Room = room,
                    RoomNumber = room.RoomNumber,
                    CheckIn = checkInDate,
                    CheckOut = checkOutDate,
                    NumberOfGuests = numberOfGuests,
                    IsPaid = isPaid,
                    Guest = guest
                };

                try
                {
                    _bookingService.CreateBooking(booking);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ett fel uppstod vid bokningsskapandet: {ex.Message}");
                    if (ex.InnerException != null)
                        Console.WriteLine($"Teknisk information: {ex.InnerException.Message}");
                    Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett oväntat fel uppstod: {ex.Message}");
                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
            }
        }

        public void UpdateBooking()
        {
            Console.WriteLine("Skriv in bokningsId");
            int bookingId = Convert.ToInt32(Console.ReadLine());
            Booking? existingBooking = _bookingService.GetBooking(bookingId);
            if (existingBooking == null)
            {
                throw new Exception("Bokningen kan inte hittas.");
            }

            Console.WriteLine("Skriv in rumsnummer (lämna tomt för att behålla befintlig information)");
            string? roomNumber = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(roomNumber))
            {
                Room? room = _roomService.GetRoom(roomNumber);
                if (room == null)
                {
                    throw new Exception("Rummet existerar inte.");
                }
                existingBooking.Room = room;
            }

            Console.WriteLine("Skriv in incheckningsdatum (lämna tomt för att behålla befintlig information)");
            string? checkInDateInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(checkInDateInput))
            {
                DateTime checkInDate = Convert.ToDateTime(checkInDateInput);
                existingBooking.CheckIn = checkInDate;
            }

            Console.WriteLine("Skriv in utcheckningsdatum (lämna tomt för att behålla befintlig information)");
            string? checkOutDateInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(checkOutDateInput))
            {
                DateTime checkOutDate = Convert.ToDateTime(checkOutDateInput);
                existingBooking.CheckOut = checkOutDate;
            }

            Console.WriteLine("Skriv in antalet gäster (lämna tomt för att behålla befintlig information)");
            string? numberOfGuestsInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(numberOfGuestsInput))
            {
                int numberOfGuests = Convert.ToInt32(numberOfGuestsInput);
                existingBooking.NumberOfGuests = numberOfGuests;
            }

            Console.WriteLine("Skriv in betalningsstatus (true/false) (lämna tomt för att behålla befintlig information)");
            string? isPaidInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(isPaidInput))
            {
                bool isPaid = Convert.ToBoolean(isPaidInput);
                existingBooking.IsPaid = isPaid;
            }

            Console.WriteLine("Skriv in gästId (lämna tomt för att behålla befintlig information)");
            string? guestIdInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(guestIdInput))
            {
                int guestId = Convert.ToInt32(guestIdInput);
                Guest? guest = _guestService.GetGuest(guestId);
                if (guest == null)
                {
                    throw new Exception("Kan inte hitta gäst.");
                }
                existingBooking.Guest = guest;
            }

            _bookingService.UpdateBooking(bookingId, existingBooking);
            Console.WriteLine("Bokningen har uppdaterats.");
            Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
        }

        public BookingService Get_bookingService()
        {
            return _bookingService;
        }

        public static void DeleteBooking(BookingService _bookingService)
        {
            Console.WriteLine("Skriv in bokningsId");
            int bookingId = Convert.ToInt32(Console.ReadLine());
            Booking? booking = _bookingService.GetBooking(bookingId);

            if (booking == null)
            {
                Console.WriteLine("Bokningen kan inte hittas.");
                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
                return;
            }

            _bookingService.DeleteBooking(booking);
            Console.WriteLine("Bokningen har tagits bort.");
            Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
        }

        public void ListBookings()
        {
            if (_bookingService == null)
            {
                Console.WriteLine("Kan inte hitta några bokningar");
                return;
            }

            var bookings = _bookingService.GetBookings();

            Console.WriteLine("Booking Id\tRoom Number\tCheck In\tCheck Out\tNumber of Guests\tIs Paid\tGuest Name");

            if (bookings == null || bookings.Count == 0)
            {
                Console.WriteLine("Inga bokningar kan hittas");
            }
            else
            {
                foreach (Booking booking in bookings)
                {
                    Console.WriteLine($"{booking.BookingId}\t{booking.Room?.RoomNumber}\t{booking.CheckIn}\t{booking.CheckOut}\t{booking.NumberOfGuests}\t{booking.IsPaid}\t{booking.Guest?.FirstName + " " + booking.Guest?.LastName}");
                }
            }

            Console.WriteLine("Tryck på valfri tangent för att återgå till menyn");
            Console.ReadKey();
        }

        public void PayBooking()
        {
            Console.WriteLine("Skriv in bokningsId");
            int bookingId = Convert.ToInt32(Console.ReadLine());
            Booking? booking = _bookingService.GetBooking(bookingId);

            if (booking == null)
            {
                Console.WriteLine("Kan inte hitta bokningen");
                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
                return;
            }

            if (booking.IsPaid)
            {
                Console.WriteLine("Bokningen är redan betald.");
                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
                return;
            }

            TimeSpan timeUntilCheckIn = booking.CheckIn - DateTime.Now;
            if (timeUntilCheckIn.TotalDays > 10)
            {
                Console.WriteLine("Det är fler än 10 dagar kvar till incheckning. Vill du betala nu? (ja/nej)");
                string? response = Console.ReadLine();
                if (response?.ToLower() != "ja")
                {
                    Console.WriteLine("Du kan betala senare via alternativet i startmenyn.");
                    Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                    Console.ReadKey();
                    return;
                }
            }
            else
            {
                Console.WriteLine("Det är 10 eller färre dagar kvar till incheckning. Bokningen måste betalas nu.");
            }

            booking.IsPaid = true;
            _bookingService.UpdateBooking(bookingId, booking);
            Console.WriteLine("Bokningens betalning har gått igenom.");
            Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
        }

        private static string GetValidRoomNumber(RoomService roomService)
        {
            while (true)
            {
                Console.Write("Skriv in rumsnummer: ");
                string? roomNumber = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(roomNumber) && roomService.GetRoom(roomNumber) != null)
                    return roomNumber;

                Console.WriteLine("Ogiltigt rumsnummer. Försök igen.");
                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
            }
        }

        private static DateTime GetValidDate(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? dateInput = Console.ReadLine();

                if (DateTime.TryParse(dateInput, out DateTime date))
                {
                    if (date.Date >= DateTime.Now.Date)
                        return date;
                    else
                        Console.WriteLine("Datumet måste vara idag eller i framtiden.");
                }
                else
                    Console.WriteLine("Ogiltigt datum. Använd formatet ÅÅÅÅ-MM-DD.");
                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
            }
        }

        private static int GetValidNumberOfGuests(Room room)
        {
            int maxGuests = (room.Bed == Room.BedType.Enkelsäng ? 1 : 2) + room.ExtraBed;
            while (true)
            {
                Console.Write("Skriv antalet gäster: ");
                if (int.TryParse(Console.ReadLine(), out int guests) && guests > 0 && guests <= maxGuests)
                    return guests;

                Console.WriteLine($"Ogiltigt antal gäster. Ange ett positivt heltal upp till {maxGuests}.");
                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
            }
        }

        private static int GetValidGuestId()
        {
            while (true)
            {
                Console.Write("Skriv in gästId: ");
                if (int.TryParse(Console.ReadLine(), out int guestId) && guestId > 0)
                    return guestId;

                Console.WriteLine("Ogiltigt gästID. Ange ett positivt heltal.");
                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
            }
        }
    }
}