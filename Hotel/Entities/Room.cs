using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Entities
{
    public class Room
    {
        [Key]
        public int RoomNumber { get; set; }
        public List<Booking> Bookings { get; set; }
        public int RoomSize { get; set; } 
        public enum BedType
        {   Enkelsäng, 
            Dubbelsäng,
        }
        public BedType Bed { get; set; }
        public int ExtraBed { get; set; }
    }
}
