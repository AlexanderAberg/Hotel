using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Entities
{
    public class Booking
    {
        public int BookingID { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int NumberOfGuests { get; set; }
        public bool IsPaid { get; set; }
        public List<Room> Rooms { get; set; }
        public List<Guest> Guests { get; set; }
    }
}
