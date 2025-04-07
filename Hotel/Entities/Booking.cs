using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Entities
{
    public class Booking
    {
        public Booking()
        {
            Room = new Room { RoomNumber = string.Empty };
            Guest = new Guest
            {
                FirstName = string.Empty,
                LastName = string.Empty,
                Email = string.Empty,
                City = string.Empty,
                Phone = string.Empty
            };
            RoomNumber = string.Empty;
        }

        public int BookingId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int NumberOfGuests { get; set; }
        public bool IsPaid { get; set; }
        public virtual Room Room { get; set; }
        public virtual Guest Guest { get; set; }
        public bool IsAvailable { get; internal set; }

        public required string RoomNumber { get; set; }
        public int GuestId { get; set; }
        public DateTime PaymentDueDate { get; internal set; }

        public string GetFormattedBookingId()
        {
            return BookingId.ToString("D7");
        }
    }
}
