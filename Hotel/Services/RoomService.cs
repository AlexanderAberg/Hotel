using Hotel.Data;
using Hotel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services
{
    public class RoomService
    {
        ApplicationDbContext _dbContext;


        public RoomService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateRoom(Room room)
        {

            _dbContext.Rooms.Add(room);
            _dbContext.SaveChanges();
        }


        public Room GetRoom(int roomNumber)
        {
            return null;
        }


        public List<Room> GetAllRooms()
        {
            return null;
        }


        public string UpdateRoom(int roomNumber)
        {

            return "Return status message (success or failure)";
        }

        public string DeleteRoom()
        {
            return "Return status message (success or failure)";
        }


        public List<Room> GetRooms()
        {
            return _dbContext.Rooms.ToList();
        }
    }
}
