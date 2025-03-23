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
        public Menu(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Start()
        {
            Console.WriteLine("Main Menu");
            Console.WriteLine("****************");
            Console.WriteLine("1. Guests");
            Console.WriteLine("2. Rooms");
            Console.WriteLine("3. Bookings");
            Console.WriteLine("4. Check in guest");
            Console.WriteLine("5. Check out guest");
            Console.WriteLine("6. Guest paid for booking");
            Console.WriteLine("0. Exit");
            var choice = Console.ReadLine();

            var guestController = new GuestController(new GuestService(_dbContext));


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
                    Console.WriteLine("Invalid choice");
                    PressAnyKeyToContinue();
                    break;
            }
        }

        public void GuestMenu()
        {
            Console.WriteLine("Guest Menu");
            Console.WriteLine("****************");
            Console.WriteLine("1. Create guest");
            Console.WriteLine("2. Update guest");
            Console.WriteLine("3. Delete guest");
            Console.WriteLine("4. List guests");
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
                    Console.WriteLine("Invalid choice");
                    PressAnyKeyToContinue();
                    break;
            }
        }

        public void RoomMenu()
        {
            Console.WriteLine("Room Menu");
            Console.WriteLine("****************");
            Console.WriteLine("1. Create room");
            Console.WriteLine("2. Update room");
            Console.WriteLine("3. Delete room");
            Console.WriteLine("4. List rooms");
            Console.WriteLine("0. Exit");
            var choice = Console.ReadLine();
            var guestController = new GuestController(new GuestService(_dbContext));


            switch (choice)
            {
                case "1":
                    Console.Clear();
                    guestController.CreateRoom();
                    break;
                case "2":
                    Console.Clear();
                    guestController.UpdateRoom();
                    break;
                case "3":
                    Console.Clear();
                    guestController.DeleteRoom();
                    break;
                case "4":
                    Console.Clear();
                    guestController.ListRooms();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    PressAnyKeyToContinue();
                    break;
            }
        }

        public void BookingMenu()
        {
            Console.WriteLine("Booking Menu");
            Console.WriteLine("****************");
            Console.WriteLine("1. Create booking");
            Console.WriteLine("2. Update booking");
            Console.WriteLine("3. Delete booking");
            Console.WriteLine("4. List bookings");
            Console.WriteLine("0. Exit");
            var choice = Console.ReadLine();
            var guestController = new GuestController(new GuestService(_dbContext));
            
            
            switch (choice)
            {
                case "1":
                    Console.Clear();
                    guestController.CreateBooking();
                    break;
                case "2":
                    Console.Clear();
                    guestController.UpdateBooking();
                    break;
                case "3":
                    Console.Clear();
                    guestController.DeleteBooking();
                    break;
                case "4":
                    Console.Clear();
                    guestController.ListBookings();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    PressAnyKeyToContinue();
                    break;
            }

        }

        public void PressAnyKeyToContinue()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
