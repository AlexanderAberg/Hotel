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
            if (guest == null)
            {
                string message = "Skapande misslyckades: guest är null.";
                Console.WriteLine(message);
                Console.ReadLine();
                return;
            }

            _dbContext.Guests.Add(guest);
            _dbContext.SaveChanges();
            string successMessage = $"Gäst med ID {guest.GuestId} skapades framgångsrikt.";
            Console.WriteLine(successMessage);
            Console.ReadLine();
        }


        public Guest? GetGuest(int guestId)
        {
            var guest = _dbContext.Guests.Find(guestId);
            if (guest == null)
            {
                Console.WriteLine($"Gäst med ID {guestId} kan inte hittas.");
                Console.ReadLine();
                return null;
            }
            return guest;
        }


        public List<Guest> GetAllGuests()
        {
            return _dbContext.Guests.ToList();
        }


        public string UpdateGuest(int guestId, Guest updatedGuest)
        {
            if (updatedGuest == null)
            {
                string message = "Uppdatering misslyckades: updatedGuest är null.";
                Console.WriteLine(message);
                Console.ReadLine();
                return message;
            }

            var guest = _dbContext.Guests.Find(guestId);
            if (guest == null)
            {
                string message = $"Uppdatering misslyckades: Gäst med ID {guestId} kan inte hittas.";
                Console.WriteLine(message);
                Console.ReadLine(); 
                return message;
            }

            guest.FirstName = updatedGuest.FirstName;
            guest.LastName = updatedGuest.LastName;
            guest.Email = updatedGuest.Email;
            guest.City = updatedGuest.City;
            guest.Phone = updatedGuest.Phone;

            _dbContext.SaveChanges();
            string successMessage = $"Gäst med ID {guestId} uppdaterades framgångsrikt.";
            Console.WriteLine(successMessage);
            Console.ReadLine(); 
            return successMessage;
        }


        public string RemoveGuest(int guestId)
        {
            var guest = _dbContext.Guests.Find(guestId);
            if (guest == null)
            {
                string message = $"Borttagning misslyckades: Gäst med ID {guestId} kan inte hittas.";
                Console.WriteLine(message);
                Console.ReadLine();
                return message;
            }
            _dbContext.Guests.Remove(guest);
            _dbContext.SaveChanges();
            string successMessage = $"Gäst med ID {guestId} borttagen.";
            Console.WriteLine(successMessage);
            Console.ReadLine();
            return successMessage;
        }


        public List<Guest> GetGuests()
        {
            return _dbContext.Guests.ToList();
        }

        internal Guest GetGuest(object guestId)
        {
            throw new NotImplementedException();
        }
    }
}
