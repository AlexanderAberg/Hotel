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
        public List<Booking> Bookings { get; set; } = new List<Booking>();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        [StringLength(15)]
        public string Phone { get; set; } = null!;
    }
}
