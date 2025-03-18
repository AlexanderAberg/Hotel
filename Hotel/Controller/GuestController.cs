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
        private GuestService _studentService;

        public GuestController(GuestService guestService)
        {
            _guestService = guestService;
        }

        public void RegisterNewStudent()
        {
            Console.WriteLine("Skriv in gästens uppgifter: ");

            Console.WriteLine($"{Environment.NewLine}Namn:");
            Console.Write(">");
            var firstName = Console.ReadLine();
            var lastName = Console.ReadLine();
            Console.Write(">");
            Console.WriteLine($"{Environment.NewLine}Email:");
            var email = Console.ReadLine();
            Console.WriteLine($"{Environment.NewLine}Stad:");
            Console.Write(">");
            var city = Console.ReadLine();
            Console.WriteLine($"{Environment.NewLine}Telefon:");
            Console.Write(">");
            var phone = Console.ReadLine();

            var availableRooms = _guestService.GetRooms();


            Console.WriteLine($"{Environment.NewLine}Välj rum:");

            var counter = 1;
            foreach (var course in availableRooms)
            {
                Console.WriteLine($"{counter}. {room.RoomNumber}");
                counter++;
            }

            var selection = ValidateSelection(availableRooms.Count);

            var selectedCourse = availableRooms[selection - 1];

            var newStudent = new Guest
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                City = city,
                Phone = phone,
                Room = selectedRoom
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
            throw new NotImplementedException();
        }

        public void RemoveGuest()
        {
            throw new NotImplementedException();
        }

        public void ListGuests()
        {
            throw new NotImplementedException();
        }
    }
}
