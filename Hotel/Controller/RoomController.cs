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
    public class RoomController(RoomService roomService)
    {
        private readonly RoomService _roomService = roomService;

        public void CreateRoom()
        {
            Console.WriteLine("Skriv in rumsinformation: ");

            Console.WriteLine($"{Environment.NewLine}Rumsnummer:");
            Console.Write(">");
            var roomNumber = Console.ReadLine();
            if (string.IsNullOrEmpty(roomNumber))
            {
                Console.WriteLine("Ogiltig inmatning för rumsnummer.");
                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
                return;
            }
            var existingRoom = _roomService.GetRoom(roomNumber);
            if (existingRoom != null)
            {
                Console.WriteLine("Rumsnumret finns redan.");
                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"{Environment.NewLine}Storlek på rummet:");
            Console.Write(">");
            var roomSizeInput = Console.ReadLine();
            if (string.IsNullOrEmpty(roomSizeInput))
            {
                Console.WriteLine("Ogiltig inmatning för rumsstorlek.");
                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
                return;
            }
            var roomSize = int.Parse(roomSizeInput);

            Console.WriteLine($"{Environment.NewLine}Sängtyp:");
            Console.Write(">");
            var bedType = Console.ReadLine();
            if (string.IsNullOrEmpty(bedType))
            {
                Console.WriteLine("Ogiltig inmatning för sängtyp.");
                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"{Environment.NewLine}Hur många extrasängar får plats:");
            Console.Write(">");
            var extraBedInput = Console.ReadLine();
            if (string.IsNullOrEmpty(extraBedInput))
            {
                Console.WriteLine("Ogiltig inmatning för extrasängar.");
                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
                return;
            }
            var extraBed = int.Parse(extraBedInput);

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

            if (string.IsNullOrEmpty(roomNumber))
            {
                Console.WriteLine("Ogiltig inmatning för rumsnummer.");
                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
                return;
            }

            var existingRoom = _roomService.GetRoom(roomNumber: roomNumber);
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
            Console.Write("> ");
            var choiceInput = Console.ReadLine();
            if (string.IsNullOrEmpty(choiceInput))
            {
                Console.WriteLine("Ogiltig inmatning.");
                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
                return;
            }
            var choice = int.Parse(choiceInput);
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Ange ny storlek på rummet:");
                    Console.Write(">");
                    var roomSizeInput = Console.ReadLine();
                    if (string.IsNullOrEmpty(roomSizeInput))
                    {
                        Console.WriteLine("Ogiltig inmatning för rumsstorlek.");
                        Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                        Console.ReadKey();
                        return;
                    }
                    existingRoom.RoomSize = int.Parse(roomSizeInput);
                    break;
                case 2:
                    Console.WriteLine("Ange ny sängtyp:");
                    Console.Write(">");
                    var bedTypeInput = Console.ReadLine();
                    if (string.IsNullOrEmpty(bedTypeInput))
                    {
                        Console.WriteLine("Ogiltig inmatning för sängtyp.");
                        Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                        Console.ReadKey();
                        return;
                    }
                    existingRoom.Bed = Enum.Parse<Room.BedType>(bedTypeInput, true);
                    break;
                case 3:
                    Console.WriteLine("Ange nytt antal extrasängar:");
                    Console.Write(">");
                    var extraBedInput = Console.ReadLine();
                    if (string.IsNullOrEmpty(extraBedInput))
                    {
                        Console.WriteLine("Ogiltig inmatning för extrasängar.");
                        Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                        Console.ReadKey();
                        return;
                    }
                    existingRoom.ExtraBed = int.Parse(extraBedInput);
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

            if (string.IsNullOrEmpty(roomNumber))
            {
                Console.WriteLine("Ogiltig inmatning för rumsnummer.");
                Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
                return;
            }

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
                    $@"Rumsnummer: {room.RoomNumber}
                                Storlek: {room.RoomSize}
                                Sängtyp: {room.Bed}
                                Extrasängar: {room.ExtraBed}");
            }
            Console.WriteLine("Tryck på valfri tangent för att återgå till menyn");
            Console.ReadKey();
        }
    }
}