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
                GenerateBookings(dbContext);
                GenerateGuests(dbContext);
                GenerateRooms(dbContext);
            }
        }

        public static bool IfAnyDataExists(ApplicationDbContext dbContext)
        {
            {
                return dbContext.Bookings.Any() || dbContext.Guests.Any() || dbContext.Rooms.Any();
            }
        }

        public static void GenerateGuests(ApplicationDbContext dbContext)
        {
            
            dbContext.Guests.Add(new Guest
            {
                FirstName = "Kalle",
                LastName = "Berg",
                Email = "kalle@hotmail.se",
                City = "Stockholm",
                Phone = "07o012334567",
                Booking = dbContext.Bookings.FirstOrDefault(c => c.BookingId == 100001)!
            });

            dbContext.Guests.Add(new Guest
            {
                FirstName = "Anna",
                LastName = "Andersson",
                Email = "anna@hotmail.com",
                City = "Stockholm",
                Phone = "07o4122339567",
                Booking = dbContext.Bookings.FirstOrDefault(c => c.BookingId == 100002)!
            });

            dbContext.Guests.Add(new Guest
            {
                FirstName = "Joel",
                LastName = "Abraha",
                Email = "joel@hotmail.com",
                City = "Sundbyberg",
                Phone = "07o4122332567",
                Booking = dbContext.Bookings.FirstOrDefault(c => c.BookingId == 100003)!
            });

            dbContext.Guests.Add(new Guest
            {
                FirstName = "Max",
                LastName = "Häll",
                Email = "max@hotmail.com",
                City = "Sundbyberg",
                Phone = "07o4122332647",
                Booking = dbContext.Bookings.FirstOrDefault(c => c.BookingId == 100004)!
            });

            dbContext.SaveChanges();
        }


        public static void GenerateRooms(ApplicationDbContext dbContext)
        {

            dbContext.Rooms.Add(new Room
            {
                RoomNumber = 11,
                RoomSize = 10,
                BedType = "Enkelsäng",
                ExtraBed = 0,
                Booking = dbContext.Bookings.FirstOrDefault(c => c.IsAvailable == true)!
            });

            dbContext.Rooms.Add(new Room
            {
                RoomNumber = 22,
                RoomSize = 19,
                BedType = "Dubbelsäng",
                ExtraBed = 0,
                Booking = dbContext.Bookings.FirstOrDefault(c => c.IsAvailable == true)!
            });

            dbContext.Rooms.Add(new Room
            {
                RoomNumber = 33,
                RoomSize = 25,
                BedType = "Dubbelsäng",
                ExtraBed = 2,
                Booking = dbContext.Bookings.FirstOrDefault(c => c.IsAvailable == true)!
            });

            dbContext.Rooms.Add(new Room
            {
                RoomNumber = 22,
                RoomSize = 19,
                BedType = "Dubbelsäng",
                ExtraBed = 1,
                Booking = dbContext.Bookings.FirstOrDefault(c => c.IsAvailable == true)!
            });

            dbContext.SaveChanges();
        }
    }
}
