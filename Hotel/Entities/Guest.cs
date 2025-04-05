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
        public int GuestId { get; set; }
        public List<Booking> Bookings { get; set; } = [];
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string City { get; set; }
        [StringLength(15)]
        public required string Phone { get; set; } = null!;
    }
}
