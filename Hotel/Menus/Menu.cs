using Hotel.Data;
using Hotel.Services;
using Hotel.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Menus
{
    public class Menu
    {
        private ApplicationDbContext _dbContext;
        private object bookingController;

        public Menu(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Start()
        {
            Console.WriteLine("Huvudmenyn");
            Console.WriteLine("****************");
            Console.WriteLine("1. Gäster");
            Console.WriteLine("2. Rum");
            Console.WriteLine("3. Bokning");
            Console.WriteLine("4. Checka in gäst");
            Console.WriteLine("5. Checka ut gäst");
            Console.WriteLine("6. Gästbetalning för bokning");
            Console.WriteLine("0. Avsluta");
            var choice = Console.ReadLine();

            var guestController = new GuestController(new GuestService(_dbContext));
            var roomController = new RoomController(new RoomService(_dbContext));
            var bookingController = new BookingController(new BookingService(_dbContext));


            switch (choice)
            {
                case "1":
                    Console.Clear();
                    GuestMenu();
                    break;
                case "2":
                    Console.Clear();
                    RoomMenu();
                    break;
                case "3":
                    Console.Clear();
                    BookingMenu();
                    break;
                case "4":
                    Console.Clear();
                    guestController.CheckInGuest();
                    break;
                case "5":
                    Console.Clear();
                    guestController.CheckOutGuest();
                    break;
                case "6":
                    Console.Clear();
                    guestController.GuestPaidBooking();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Ogiltigt val");
                    PressAnyKeyToContinue();
                    break;
            }
        }

        public void GuestMenu()
        {
            Console.WriteLine("Gästmenyn");
            Console.WriteLine("****************");
            Console.WriteLine("1. Skapa gäst");
            Console.WriteLine("2. Uppdatera gäst");
            Console.WriteLine("3. Ta bort gäst");
            Console.WriteLine("4. Gästlista");
            Console.WriteLine("0. Exit");
            var choice = Console.ReadLine();

            var guestController = new GuestController(new GuestService(_dbContext));


            switch (choice)
            {
                case "1":
                    Console.Clear();
                    guestController.RegisterNewGuest();
                    break;
                case "2":
                    Console.Clear();
                    guestController.EditGuestInformation();
                    break;
                case "3":
                    Console.Clear();
                    guestController.RemoveGuest();
                    break;
                case "4":
                    Console.Clear();
                    guestController.ListGuests();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Ogiltigt val");
                    PressAnyKeyToContinue();
                    break;
            }
        }

        public void RoomMenu()
        {
            Console.WriteLine("Rumsmenyn");
            Console.WriteLine("****************");
            Console.WriteLine("1. Skapa rum");
            Console.WriteLine("2. Uppdatera rum");
            Console.WriteLine("3. Ta bort rum");
            Console.WriteLine("4. Rumslista");
            Console.WriteLine("0. Avsluta");
            var choice = Console.ReadLine();
            var roomController = new RoomController(new RoomService(_dbContext));


            switch (choice)
            {
                case "1":
                    Console.Clear();
                    roomController.CreateRoom();
                    break;
                case "2":
                    Console.Clear();
                    roomController.UpdateRoom();
                    break;
                case "3":
                    Console.Clear();
                    roomController.DeleteRoom();
                    break;
                case "4":
                    Console.Clear();
                    roomController.ListRooms();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Ogiltigt val");
                    PressAnyKeyToContinue();
                    break;
            }
        }

        public void BookingMenu()
        {
            Console.WriteLine("Bokningsmeny");
            Console.WriteLine("****************");
            Console.WriteLine("1. Skapa bokning");
            Console.WriteLine("2. Uppdatera bokning");
            Console.WriteLine("3. Ta bort bokning");
            Console.WriteLine("4. Bokningslista");
            Console.WriteLine("5. Betala bokning");
            Console.WriteLine("0. Exit");
            var choice = Console.ReadLine();
            var bookingController = new BookingController(new BookingService(_dbContext));
            
            
            switch (choice)
            {
                case "1":
                    Console.Clear();
                    bookingController.CreateBooking();
                    break;
                case "2":
                    Console.Clear();
                    bookingController.UpdateBooking();
                    break;
                case "3":
                    Console.Clear();
                    bookingController.DeleteBooking();
                    break;
                case "4":
                    Console.Clear();
                    bookingController.ListBookings();
                    break;
                case "5":
                    Console.Clear();
                    bookingController.PayBooking();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Ogiltigt val");
                    PressAnyKeyToContinue();
                    break;
            }

        }

        public void PressAnyKeyToContinue()
        {
            Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
        }
    }
}
