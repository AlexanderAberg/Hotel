using Hotel.Data;
using Hotel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services
{
    public class GuestService
    {
        ApplicationDbContext _dbContext;


        public GuestService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateGuest(Guest guest)
        {

            _dbContext.Guests.Add(guest);
            _dbContext.SaveChanges();
        }


        public Guest GetGuest(int guestId)
        {
            return null;
        }


        public List<Guest> GetAllGuests()
        {
            return null;
        }


        public string UpdateGuest(int guestId)
        {

            return "Return status message (success or failure)";
        }

        public string RemoveGuest(int guestId)
        {
            var guest = _dbContext.Guests.Find(guestId);
            if (guest != null)
            {
                _dbContext.Guests.Remove(guest);
                _dbContext.SaveChanges();
                return "Guest removed successfully.";
            }
            return "Guest not found.";
        }


        public List<Guest> GetGuests()
        {
            return _dbContext.Guests.ToList();
        }
    }
}
