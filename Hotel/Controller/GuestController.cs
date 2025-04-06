using Hotel.Entities;
using Hotel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Controller
{
    public class GuestController(GuestService guestService)
    {
        private readonly GuestService _guestService = guestService;
        private readonly RoomService? _roomService;
        private readonly BookingService? _bookingService;

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
                        if (!booking.IsPaid)
                        {
                            var daysUntilCheckIn = (booking.CheckIn - DateTime.Now).TotalDays;
                            if (daysUntilCheckIn <= 10)
                            {
                                booking.IsPaid = true;
                                _bookingService?.UpdateBooking(booking.BookingId, booking);
                                Console.WriteLine("Bokningen har markerats som betald!");
                                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                                Console.ReadKey();
                            }
                            else if (daysUntilCheckIn > 10 && (DateTime.Now - booking.CheckIn).TotalDays <= 10)
                            {
                                booking.IsPaid = true;
                                _bookingService?.UpdateBooking(booking.BookingId, booking);
                                Console.WriteLine("Bokningen har markerats som betald!");
                                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("Bokningen har avbrutits eftersom betalningen inte gjordes inom 10 dagar.");
                                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                                Console.ReadKey();
                                _bookingService?.DeleteBooking(booking);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Bokningen är redan betald.");
                            Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Gästen har ingen bokning.");
                        Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Gäst hittades inte.");
                    Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Ogiltigt ID.");
                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
            }
        }

        public void RegisterNewGuest()
        {
            Console.WriteLine("Skriv in gästens uppgifter: ");

            Console.WriteLine($"{Environment.NewLine}Förnamn:");
            Console.Write(">");
            var firstName = Console.ReadLine() ?? string.Empty;
            Console.WriteLine($"{Environment.NewLine}Efternamn:");
            Console.Write(">");
            var lastName = Console.ReadLine() ?? string.Empty;
            Console.Write(">");
            Console.WriteLine($"{Environment.NewLine}Email:");
            var email = Console.ReadLine() ?? string.Empty;
            Console.WriteLine($"{Environment.NewLine}Ort:");
            Console.Write(">");
            var city = Console.ReadLine() ?? string.Empty;
            Console.WriteLine($"{Environment.NewLine}Telefon:");
            Console.Write(">");
            var phone = Console.ReadLine() ?? string.Empty;

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
            Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
            Console.ReadKey();

        }

        public static int ValidateSelection(int selectionMenuLimit)
        {
            Console.WriteLine($"{Environment.NewLine}Välj i menyn:");
            while (true)
            {
                Console.Write("> ");
                if (int.TryParse(Console.ReadLine(), out int intSelection) && intSelection >= 0 && intSelection <= selectionMenuLimit)
                    return intSelection;



                Console.WriteLine("Fel val");
                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
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

                    var result = _guestService.UpdateGuest(guestId, guest);
                    Console.WriteLine(result);
                }
                else
                {
                    Console.WriteLine("Gäst hittades inte.");
                    Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Ogiltigt ID.");
                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
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
                    if (guest.Bookings != null && guest.Bookings.Count != 0)
                    {
                        Console.WriteLine("Gästen har aktiva bokningar. Vill du ta bort dem också? (ja/nej)");
                        Console.Write("> ");
                        var response = Console.ReadLine();
                        if (response?.ToLower() == "ja")
                        {
                            foreach (var booking in guest.Bookings)
                            {
                                _bookingService?.DeleteBooking(booking);
                            }
                            Console.WriteLine("Alla gästens bokningar har tagits bort.");
                            Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Det finns aktiva bokningar på gästen, därför har gästen inte tagits bort.");
                            Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                            Console.ReadKey();
                            return;
                        }
                    }
                    _guestService.RemoveGuest(guestId);
                    Console.WriteLine("Gästen har tagits bort.");
                    Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Gäst hittades inte.");
                    Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Ogiltigt ID.");
                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
            }
        }

        public void ListGuests()
        {
            var guests = _guestService.GetGuests();
            if (guests == null)
            {
                Console.WriteLine("Gästlistan är tom.");

                return;
            }

            if (guests.Count != 0)
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
            Console.WriteLine("Tryck på valfri tangent för att återgå till menyn");
            Console.ReadKey();
        }
    }
}
