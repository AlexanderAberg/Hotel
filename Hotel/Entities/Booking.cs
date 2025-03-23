using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Entities
{
    public class Booking
    {
        private static int _nextBookingId = 0000001;

        public Booking()
        {
            BookingId = _nextBookingId++;
        }

        public int BookingId { get; private set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int NumberOfGuests { get; set; }
        public bool IsPaid { get; set; }
        public Room Room { get; set; }
        public Guest Guest { get; set; }
        public bool IsAvailable { get; internal set; }

        public string GetFormattedBookingId()
        {
            return BookingId.ToString("D7");
        }
    }
}
