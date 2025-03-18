using Hotel.Data;
using Hotel.Services;
using Hotel.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Menu
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

            Console.WriteLine("1. Create guest");
            Console.WriteLine("2. Update guest");
            Console.WriteLine("3. Delete guest");
            Console.WriteLine("4. List guest");
            Console.WriteLine("5. Check in guest");
            Console.WriteLine("6. Check out guest");
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
                case "5":
                    Console.Clear();
                    guestController.CheckInGuest();
                    break;
                case "6":
                    Console.Clear();
                    guestController.CheckOutGuest();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }
}
