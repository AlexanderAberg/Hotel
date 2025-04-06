using Hotel.Menus;
using Hotel.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Hotel.Controller;
using Hotel.Services;

namespace Hotel
{
    public class Program
    {
        static void Main()
        {
            using var dbContext = DataInitializer.Build();
            DataInitializer.InitializeData(dbContext);
            var bookingService = new BookingService(dbContext);
            bookingService.RemoveUnpaidBookings();
            var menu = new Menu(dbContext);
            menu.Start(new RoomController(new RoomService(dbContext)));
        }
    }
}
