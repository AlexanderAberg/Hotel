using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Data
{
    internal class ApplicationDbContex : DbContext
    {
        public DbSet<Entities.Booking> Bookings { get; set; } = null!;
        public DbSet<Entities.Guest> Guests { get; set; } = null!;
        public DbSet<Entities.Room> Rooms { get; set; } = null!;
    }
}
