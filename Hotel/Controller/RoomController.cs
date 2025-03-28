using Hotel.Data;
using Hotel.Menus;
using Hotel.Entities;
using Hotel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Controller
{
    public class RoomController
    {
        private RoomService _roomService;

        public RoomController(RoomService roomService)
        {
            _roomService = roomService;
        }
        public void CreateRoom()
        {
            Console.WriteLine("Skriv in rumsinformation: ");

            Console.WriteLine($"{Environment.NewLine}Rumsnummer:");
            Console.Write(">");
            var roomNumber = Console.ReadLine();

            Console.WriteLine($"{Environment.NewLine}Storlek på rummet:");
            Console.Write(">");
            var roomSize = int.Parse(Console.ReadLine());
            Console.WriteLine($"{Environment.NewLine}Sängtyp:");
            Console.Write(">");
            var bedType = Console.ReadLine();
            Console.WriteLine($"{Environment.NewLine}Hur många extrasängar får plats:");
            Console.Write(">");
            var extraBed = int.Parse(Console.ReadLine());

            var newRoom = new Room
            {
                RoomNumber = roomNumber,
                RoomSize = roomSize,
                Bed = Enum.Parse<Room.BedType>(bedType, true),
                ExtraBed = extraBed,
            };

            _roomService.CreateRoom(newRoom);
            Console.WriteLine("Rummet är tillagt");
            Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
        }

        public void UpdateRoom()
        {
            Console.WriteLine("Vilket rum vill du uppdatera? Ange rumsnummer:");
            Console.Write(">");
            var roomNumber = Console.ReadLine();

            var existingRoom = _roomService.GetRoom(roomNumber);
            if (existingRoom == null)
            {
                Console.WriteLine("Rummet finns inte.");
                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Vilken information vill du uppdatera? Ange siffra:" +
                              $"{Environment.NewLine}1. Storlek på rummet" +
                              $"{Environment.NewLine}2. Sängtyp" +
                              $"{Environment.NewLine}3. Antal extrasängar");
            Console.Write(">");
            var choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Ange ny storlek på rummet:");
                    Console.Write(">");
                    existingRoom.RoomSize = int.Parse(Console.ReadLine());
                    break;
                case 2:
                    Console.WriteLine("Ange ny sängtyp:");
                    Console.Write(">");
                    existingRoom.Bed = Enum.Parse<Room.BedType>(Console.ReadLine(), true);
                    break;
                case 3:
                    Console.WriteLine("Ange nytt antal extrasängar:");
                    Console.Write(">");
                    existingRoom.ExtraBed = int.Parse(Console.ReadLine());
                    break;
                default:
                    Console.WriteLine("Ogiltigt val.");
                    Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                    Console.ReadKey();
                    return;
            }

            var result = _roomService.UpdateRoom(roomNumber, existingRoom);
            Console.WriteLine(result);
            Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
        }

        public void DeleteRoom()
        {
            Console.WriteLine("Vilket rum vill du ta bort? Ange rumsnummer:");
            Console.Write(">");
            var roomNumber = Console.ReadLine();

            var existingRoom = _roomService.GetRoom(roomNumber);
            if (existingRoom == null)
            {
                Console.WriteLine("Rummet finns inte.");
                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
                return;
            }

            var result = _roomService.DeleteRoom(existingRoom);
            Console.WriteLine(result);
            Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
        }



        public void ListRooms()
        {
            var rooms = _roomService.GetRooms();
            foreach (var room in rooms)
            {
                Console.WriteLine(
                    $"Rumsnummer: {room.RoomNumber}" +
                    $"{Environment.NewLine}Storlek: {room.RoomSize}" +
                    $"{Environment.NewLine}Sängtyp: {room.Bed}" +
                    $"{Environment.NewLine}Extrasängar: {room.ExtraBed}");
            }
            Console.WriteLine("Tryck på valfri tangent för att återgå till menyn");
            Console.ReadKey();
        }
    }
}
