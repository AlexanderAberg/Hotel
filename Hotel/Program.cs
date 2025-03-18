using Hotel.Menu;
using Hotel.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EFCore_Code_First_Tutorial.Data;

namespace Hotel
{
    internal class Program
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
