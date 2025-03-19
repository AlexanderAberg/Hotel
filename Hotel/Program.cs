using Hotel.Menus;
using Hotel.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Hotel
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = DataInitializer.Build())
            {
                DataInitializer.InitializeData(dbContext);
                var menu = new Menu(dbContext);
                menu.Start();
            }
        }
    }
}
