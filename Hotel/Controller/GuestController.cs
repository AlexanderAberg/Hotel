using Hotel.Entities;
using Hotel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Controller
{
    public class GuestController
    {
        private GuestService _guestService;
        private RoomService _roomService;
        private BookingService _bookingService;

        public GuestController(GuestService guestService)
        {
            _guestService = guestService;
        }

        public void CheckInGuest()
        {
            Console.WriteLine("Ange gästens ID för att checka in gästen:");
            Console.Write("> ");
            if (int.TryParse(Console.ReadLine(), out int guestId))
            {
                var guest = _guestService.GetGuest(guestId);
                if (guest != null)
                {
                    var booking = guest.Bookings.FirstOrDefault();
                    if (booking != null && booking.Room != null)
                    {
                        if (booking.CheckIn.Date == DateTime.Now.Date)
                        {
                            var room = booking.Room;
                            if (room.Bookings.All(b => b.CheckOut <= DateTime.Now))
                            {
                                room.Bookings.Add(booking);
                                booking.CheckIn = DateTime.Now;
                                _bookingService.UpdateBooking(booking.BookingId, booking);
                                Console.WriteLine("Gästen har checkats in!");
                            }
                            else
                            {
                                Console.WriteLine("Rummet är redan upptaget.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Du kan bara checka in på incheckningsdagen.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Gästen har inga bokade rum.");
                    }
                }
                else
                {
                    Console.WriteLine("Gäst hittades inte.");
                }
            }
            else
            {
                Console.WriteLine("Ogiltigt ID.");
            }
        }

        public void CheckOutGuest()
        {
            Console.WriteLine("Ange gästens ID för att checka ut gästen:");
            Console.Write("> ");
            if (int.TryParse(Console.ReadLine(), out int guestId))
            {
                var guest = _guestService.GetGuest(guestId);
                if (guest != null)
                {
                    var booking = guest.Bookings.FirstOrDefault();
                    if (booking != null && booking.Room != null)
                    {
                        if (booking.CheckOut.Date == DateTime.Now.Date)
                        {
                            var room = booking.Room;
                            if (room.Bookings.Contains(booking))
                            {
                                room.Bookings.Remove(booking);
                                booking.CheckOut = DateTime.Now;
                                _bookingService.UpdateBooking(booking.BookingId, booking);
                                Console.WriteLine("Gästen har checkats ut!");
                            }
                            else
                            {
                                Console.WriteLine("Rummet är redan tomt.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Du kan bara checka ut på utcheckningsdagen.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Gästen har inga bokade rum.");
                    }
                }
                else
                {
                    Console.WriteLine("Gäst hittades inte.");
                }
            }
            else
            {
                Console.WriteLine("Ogiltigt ID.");
            }
        }

        public void GuestPaidBooking()
        {
            Console.WriteLine("Ange gästens ID för att markera att gästen har betalat för bokningen:");
            Console.Write("> ");
            if (int.TryParse(Console.ReadLine(), out int guestId))
            {
                var guest = _guestService.GetGuest(guestId);
                if (guest != null)
                {
                    var booking = guest.Bookings.FirstOrDefault();
                    if (booking != null)
                    {
                        var daysUntilCheckIn = (booking.CheckIn - DateTime.Now).TotalDays;
                        if (daysUntilCheckIn <= 10)
                        {
                            booking.IsPaid = true;
                            _bookingService.UpdateBooking(booking.BookingId, booking);
                            Console.WriteLine("Bokningen har markerats som betald!");
                        }
                        else if (daysUntilCheckIn > 10 && (DateTime.Now - booking.CheckIn).TotalDays <= 10)
                        {
                            booking.IsPaid = true;
                            _bookingService.UpdateBooking(booking.BookingId, booking);
                            Console.WriteLine("Bokningen har markerats som betald!");
                        }
                        else
                        {
                            Console.WriteLine("Bokningen har avbrutits eftersom betalningen inte gjordes inom 10 dagar.");
                            _bookingService.DeleteBooking(booking);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Gästen har ingen bokning.");
                    }
                }
                else
                {
                    Console.WriteLine("Gäst hittades inte.");
                }
            }
            else
            {
                Console.WriteLine("Ogiltigt ID.");
            }
        }

        public void RegisterNewGuest()
        {
            Console.WriteLine("Skriv in gästens uppgifter: ");

            Console.WriteLine($"{Environment.NewLine}Förnamn:");
            Console.Write(">");
            var firstName = Console.ReadLine();
            Console.WriteLine($"{Environment.NewLine}Förnamn:");
            Console.Write(">");
            var lastName = Console.ReadLine();
            Console.Write(">");
            Console.WriteLine($"{Environment.NewLine}Email:");
            var email = Console.ReadLine();
            Console.WriteLine($"{Environment.NewLine}Ort:");
            Console.Write(">");
            var city = Console.ReadLine();
            Console.WriteLine($"{Environment.NewLine}Telefon:");
            Console.Write(">");
            var phone = Console.ReadLine();

            var availableBookings = _roomService.GetRooms();


            Console.WriteLine($"{Environment.NewLine}Välj rum:");

            var counter = 1;
            foreach (var booking in availableBookings)
            {
                Console.WriteLine($"{counter}. Room Number: {booking.RoomNumber}, Room Size: {booking.RoomSize}, Bed Type: {booking.Bed}");
                counter++;
            }

            var selection = ValidateSelection(availableBookings.Count);

            var selectedBooking = availableBookings[selection - 1];

            var newGuest = new Guest
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                City = city,
                Phone = phone,
            };

            _guestService.CreateGuest(newGuest);

            Console.WriteLine("Gästen har registrerats!");

        }

        public static int ValidateSelection(int selectionMenuLimit)
        {
            int intSelection;
            Console.WriteLine($"{Environment.NewLine}Välj i menyn:");
            while (true)
            {
                Console.Write("> ");
                if (int.TryParse(Console.ReadLine(), out intSelection) && intSelection >= 0 && intSelection <= selectionMenuLimit)
                    return intSelection;



                Console.WriteLine("Fel val");
            }

        }

        public void EditGuestInformation()
        {
            Console.WriteLine("Ange gästens ID för att redigera gästens information:");
            Console.Write("> ");
            if (int.TryParse(Console.ReadLine(), out int guestId))
            {
                var guest = _guestService.GetGuest(guestId);
                if (guest != null)
                {
                    Console.WriteLine($"{Environment.NewLine}Nuvarande information:");
                    Console.WriteLine($"Förnamn: {guest.FirstName}");
                    Console.WriteLine($"Efternamn: {guest.LastName}");
                    Console.WriteLine($"Email: {guest.Email}");
                    Console.WriteLine($"Ort: {guest.City}");
                    Console.WriteLine($"Telefon: {guest.Phone}");

                    Console.WriteLine($"{Environment.NewLine}Ange ny information (lämna tomt för att behålla nuvarande information):");

                    Console.WriteLine($"{Environment.NewLine}Förnamn:");
                    Console.Write("> ");
                    var firstName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(firstName)) guest.FirstName = firstName;

                    Console.WriteLine($"{Environment.NewLine}Efternamn:");
                    Console.Write("> ");
                    var lastName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(lastName)) guest.LastName = lastName;

                    Console.WriteLine($"{Environment.NewLine}Email:");
                    Console.Write("> ");
                    var email = Console.ReadLine();
                    if (!string.IsNullOrEmpty(email)) guest.Email = email;

                    Console.WriteLine($"{Environment.NewLine}Stad:");
                    Console.Write("> ");
                    var city = Console.ReadLine();
                    if (!string.IsNullOrEmpty(city)) guest.City = city;

                    Console.WriteLine($"{Environment.NewLine}Telefon:");
                    Console.Write("> ");
                    var phone = Console.ReadLine();
                    if (!string.IsNullOrEmpty(phone)) guest.Phone = phone;

                    _guestService.UpdateGuest(guestId);
                    Console.WriteLine("Gästinformation har uppdaterats!");
                }
                else
                {
                    Console.WriteLine("Gäst hittades inte.");
                }
            }
            else
            {
                Console.WriteLine("Ogiltigt ID.");
            }
        }

        public void RemoveGuest()
        {
            Console.WriteLine("Ange gästens ID för att ta bort gästen:");
            Console.Write("> ");
            if (int.TryParse(Console.ReadLine(), out int guestId))
            {
                var guest = _guestService.GetGuest(guestId);
                if (guest != null)
                {
                    if (guest.Bookings.Any())
                    {
                        Console.WriteLine("Gästen har aktiva bokningar. Vill du ta bort dem också? (ja/nej)");
                        Console.Write("> ");
                        var response = Console.ReadLine();
                        if (response?.ToLower() == "ja")
                        {
                            foreach (var booking in guest.Bookings)
                            {
                                _bookingService.DeleteBooking(booking);
                            }
                            Console.WriteLine("Alla gästens bokningar har tagits bort.");
                        }
                        else
                        {
                            Console.WriteLine("Det finns aktiva bokningar på gästen, därför har gästen inte tagits bort.");
                            return;
                        }
                    }
                    _guestService.RemoveGuest();
                    Console.WriteLine("Gästen har tagits bort.");
                }
                else
                {
                    Console.WriteLine("Gäst hittades inte.");
                }
            }
            else
            {
                Console.WriteLine("Ogiltigt ID.");
            }
        }

        public void ListGuests()
        {
            var guests = _guestService.GetGuests();
            if (guests.Any())
            {
                foreach (var guest in guests)
                {
                    Console.WriteLine($"ID: {guest.GuestId}, Name: {guest.FirstName} {guest.LastName}, Email: {guest.Email}, City: {guest.City}, Phone: {guest.Phone}");
                }
            }
            else
            {
                Console.WriteLine("Inga gäster hittades.");
            }
        }
    }
}
