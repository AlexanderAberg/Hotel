using Hotel.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_Code_First_Tutorial.Data
{
    public class DataInitializer
    {
        public static void Build()
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseSqlServer(connectionString)
             .Options;

            using var dbContext = new ApplicationDbContext(contextOptions);
        }
    }
}
