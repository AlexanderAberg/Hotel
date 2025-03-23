using Hotel.Data;
using Hotel.Entities;
using Hotel.Menus;
using Hotel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Controller
{
    public class BookingController
    {
        private BookingService _bookingService;

        public BookingController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }
        public void CreateBooking()
        {
            throw new NotImplementedException();
        }

        public void UpdateBooking()
        {
            throw new NotImplementedException();
        }

        public void DeleteBooking()
        {
            throw new NotImplementedException();
        }

        public void ListBookings()
        {
            throw new NotImplementedException();
        }
    }
}
