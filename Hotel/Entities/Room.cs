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
        public Booking Booking { get; set; } 
        public int RoomSize { get; set; } 
        public string BedType { get; set; } 
        public int ExtraBed { get; set; } 
    }
}
