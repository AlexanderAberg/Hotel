using Hotel.Data;
using Hotel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Data
{
    public class DataInitializer
    {
        public static ApplicationDbContext Build()
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseSqlServer(connectionString)
             .Options;

            var dbContext = new ApplicationDbContext(contextOptions);

            dbContext.Database.Migrate();

            return dbContext;
        }
        public static void InitializeData(ApplicationDbContext dbContext)
        {
            if (!IfAnyDataExists(dbContext))
            {
                GenerateGuests(dbContext);
                GenerateRooms(dbContext);
            }
        }

        public static bool IfAnyDataExists(ApplicationDbContext dbContext)
        {
            return dbContext.Guests.Any() || dbContext.Rooms.Any();
        }

        public static void GenerateGuests(ApplicationDbContext dbContext)
        {
            dbContext.Guests.AddRange(new List<Guest>
                {
                    new Guest
                    {
                        GuestId = 1,
                        FirstName = "Kalle",
                        LastName = "Berg",
                        Email = "kalle@hotmail.se",
                        City = "Stockholm",
                        Phone = "070-12344711",
                    },
                    new Guest
                    {
                        GuestId = 2,
                        FirstName = "Anna",
                        LastName = "Andersson",
                        Email = "anna@hotmail.com",
                        City = "Stockholm",
                        Phone = "070-4123957",
                    },
                    new Guest
                    {
                        GuestId = 3,
                        FirstName = "Joel",
                        LastName = "Abraha",
                        Email = "joel@hotmail.com",
                        City = "Sundbyberg",
                        Phone = "070-1233267",
                    },
                    new Guest
                    {
                        GuestId = 4,
                        FirstName = "Max",
                        LastName = "Häll",
                        Email = "max@hotmail.com",
                        City = "Sundbyberg",
                        Phone = "070-1232647",
                    }
                });

            dbContext.SaveChanges();
        }

        public static void GenerateRooms(ApplicationDbContext dbContext)
        {
            dbContext.Rooms.AddRange(new List<Room>
                {
                    new Room
                    {
                        RoomNumber = 11,
                        RoomSize = 10,
                        Bed = Room.BedType.Enkelsäng,
                        ExtraBed = 0,
                    },
                    new Room
                    {
                        RoomNumber = 12,
                        RoomSize = 12,
                        Bed = Room.BedType.Enkelsäng,
                        ExtraBed = 1,
                    },
                    new Room
                    {
                        RoomNumber = 13,
                        RoomSize = 18,
                        Bed = Room.BedType.Enkelsäng,
                        ExtraBed = 2,
                    },
                    new Room
                    {
                        RoomNumber = 14,
                        RoomSize = 17,
                        Bed = Room.BedType.Dubbelsäng,
                        ExtraBed = 0,
                    },
                    new Room
                    {
                        RoomNumber = 15,
                        RoomSize = 9,
                        Bed = Room.BedType.Enkelsäng,
                        ExtraBed = 0,
                    },
                    new Room
                    {
                        RoomNumber = 21,
                        RoomSize = 21,
                        Bed = Room.BedType.Dubbelsäng,
                        ExtraBed = 2,
                    },
                    new Room
                    {
                        RoomNumber = 22,
                        RoomSize = 19,
                        Bed = Room.BedType.Dubbelsäng,
                        ExtraBed = 0,
                    },
                    new Room
                    {
                        RoomNumber = 23,
                        RoomSize = 15,
                        Bed = Room.BedType.Dubbelsäng,
                        ExtraBed = 0,
                    },
                    new Room
                    {
                        RoomNumber = 24,
                        RoomSize = 25,
                        Bed = Room.BedType.Dubbelsäng,
                        ExtraBed = 2,
                    },
                    new Room
                    {
                        RoomNumber = 25,
                        RoomSize = 21,
                        Bed = Room.BedType.Dubbelsäng,
                        ExtraBed = 1,
                    },
                    new Room
                    {
                        RoomNumber = 31,
                        RoomSize = 19,
                        Bed = Room.BedType.Dubbelsäng,
                        ExtraBed = 1,
                    },
                    new Room
                    {
                        RoomNumber = 32,
                        RoomSize = 17,
                        Bed = Room.BedType.Dubbelsäng,
                        ExtraBed = 0,
                    },
                    new Room
                    {
                        RoomNumber = 33,
                        RoomSize = 25,
                        Bed = Room.BedType.Dubbelsäng,
                        ExtraBed = 2,
                    },
                    new Room
                    {
                        RoomNumber = 34,
                        RoomSize = 21,
                        Bed = Room.BedType.Dubbelsäng,
                        ExtraBed = 1,
                    },
                    new Room
                    {
                        RoomNumber = 35,
                        RoomSize = 10,
                        Bed = Room.BedType.Enkelsäng,
                        ExtraBed = 0,
                    },
                    new Room
                    {
                        RoomNumber = 41,
                        RoomSize = 74,
                        Bed = Room.BedType.Dubbelsäng,
                        ExtraBed = 2,
                    }
                });

            dbContext.SaveChanges();
        }
    }
}
