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

        public GuestController(GuestService guestService)
        {
            _guestService = guestService;
        }

        public void CheckInGuest()
        {
            throw new NotImplementedException();
        }

        public void CheckOutGuest()
        {
            throw new NotImplementedException();
        }

        public void GuestPaidBooking()
        {
            throw new NotImplementedException();
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

            var availableBookings = _guestService.GetRooms();


            Console.WriteLine($"{Environment.NewLine}Välj rum:");

            var counter = 1;
            foreach (var booking in availableBookings)
            {
                Console.WriteLine($"{counter}. {booking.Booking}");
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
                    if (guest.Booking != null)
                    {
                        Console.WriteLine("Gästen har aktiva bokningar. Vill du ta bort dem också? (ja/nej)");
                        Console.Write("> ");
                        var response = Console.ReadLine();
                        if (response?.ToLower() == "ja")
                        {
                            _guestService.RemoveBookings(guest.Booking);
                            Console.WriteLine("Alla gästens bokningar har tagits bort.");
                        }
                        else
                        {
                            Console.WriteLine("Det finns aktiva bokningar på gästen, därför har gästen inte tagits bort.");
                            return;
                        }
                    }
                    _guestService.DeleteGuest();
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
            throw new NotImplementedException();
        }

        public void CreateRoom()
        {
            throw new NotImplementedException();
        }

        public void UpdateRoom()
        {
            throw new NotImplementedException();
        }

        public void DeleteRoom()
        {
            throw new NotImplementedException();
        }

        public void ListRooms()
        {
            throw new NotImplementedException();
        }

        public void CreateBooking()
        {
            throw new NotImplementedException();
        }

        public void UpdateBooking()
        {
            throw new NotImplementedException();
        }

        public void DeleteBooking()
        {
            throw new NotImplementedException();
        }

        public void ListBookings()
        {
            throw new NotImplementedException();
        }
    }
}
