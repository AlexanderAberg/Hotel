﻿using Hotel.Data;
using Hotel.Services;
using Hotel.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Menus
{
    public class Menu(ApplicationDbContext dbContext)
    {
        public void Start(RoomController roomController)
        {
            ArgumentNullException.ThrowIfNull(roomController);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Huvudmenyn");
                Console.WriteLine("****************");
                Console.WriteLine("1. Gäster");
                Console.WriteLine("2. Rum");
                Console.WriteLine("3. Bokning");
                Console.WriteLine("4. Gästbetalning för bokning");
                Console.WriteLine("0. Avsluta");
                var choice = Console.ReadLine();

                var guestController = new GuestController(new GuestService(dbContext));
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
                        guestController.GuestPaidBooking();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Ogiltigt val");
                        PressAnyKeyToContinue();
                        break;
                }
            }
        }

        public void GuestMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Gästmenyn");
                Console.WriteLine("****************");
                Console.WriteLine("1. Skapa gäst");
                Console.WriteLine("2. Uppdatera gäst");
                Console.WriteLine("3. Ta bort gäst");
                Console.WriteLine("4. Gästlista");
                Console.WriteLine("0. Tillbaka");
                var choice = Console.ReadLine();

                var guestController = new GuestController(new GuestService(dbContext));

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
                        return;
                    default:
                        Console.WriteLine("Ogiltigt val");
                        PressAnyKeyToContinue();
                        break;
                }
            }
        }

        public void RoomMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Rumsmenyn");
                Console.WriteLine("****************");
                Console.WriteLine("1. Skapa rum");
                Console.WriteLine("2. Uppdatera rum");
                Console.WriteLine("3. Ta bort rum");
                Console.WriteLine("4. Rumslista");
                Console.WriteLine("0. Tillbaka");
                var choice = Console.ReadLine();
                var roomController = new RoomController(new RoomService(dbContext));

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
                        return;
                    default:
                        Console.WriteLine("Ogiltigt val");
                        PressAnyKeyToContinue();
                        break;
                }
            }
        }

        public void BookingMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Bokningsmeny");
                Console.WriteLine("****************");
                Console.WriteLine("1. Skapa bokning");
                Console.WriteLine("2. Uppdatera bokning");
                Console.WriteLine("3. Ta bort bokning");
                Console.WriteLine("4. Bokningslista");
                Console.WriteLine("5. Betala bokning");
                Console.WriteLine("0. Tillbaka");
                var choice = Console.ReadLine();
                var bookingController = new BookingController(new RoomService(dbContext),
                                                            new GuestService(dbContext),
                                                            new BookingService(dbContext));

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
                        BookingController.DeleteBooking(bookingController.Get_bookingService());
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
                        return;
                    default:
                        Console.WriteLine("Ogiltigt val");
                        PressAnyKeyToContinue();
                        break;
                }
            }
        }

        public static void PressAnyKeyToContinue()
        {
            Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
        }
    }
}