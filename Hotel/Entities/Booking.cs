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

        private DateTime _checkIn;
        public DateTime CheckIn
        {
            get => _checkIn;
            set => _checkIn = value.Date;
        }

        private DateTime _checkOut;
        public DateTime CheckOut
        {
            get => _checkOut;
            set => _checkOut = value.Date;
        }

        public int BookingId { get; set; }
        public int NumberOfGuests { get; set; }
        public bool IsPaid { get; set; }
        public virtual Room Room { get; set; }
        public virtual Guest Guest { get; set; }
        public bool IsAvailable { get; internal set; }

        public required string RoomNumber { get; set; }
        public int GuestId { get; set; }

        private DateTime _paymentDueDate;
        public DateTime PaymentDueDate
        {
            get => _paymentDueDate;
            internal set => _paymentDueDate = value.Date;
        }

        public string GetFormattedBookingId()
        {
            return BookingId.ToString("D7");
        }
    }
}
