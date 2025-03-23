using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Entities
{
    public class Guest
    {
        private static int _nextId = 5;

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GuestId { get; set; } = GenerateGuestId();
        public Booking Booking { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        [StringLength(15)]
        public string Phone { get; set; } = null!;
        public object Bookings { get; internal set; }

        private static int GenerateGuestId()
        {
            return _nextId++;
        }
    }
}
