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
        public required string RoomNumber { get; set; }
        public List<Booking>? Bookings { get; set; }
        public int RoomSize { get; set; } 
        public enum BedType
        {   Enkelsäng, 
            Dubbelsäng,
        }
        public BedType Bed { get; set; }
        [Range(0, 4, ErrorMessage = "Det går bara att välja från inga extra sängar upp till 4 extra sängar")]

        public int ExtraBed { get; set; }
    }
}
