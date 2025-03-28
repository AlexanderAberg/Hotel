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
        private readonly ApplicationDbContext _dbContext;

        public RoomService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void CreateRoom(Room room)
        {
            _dbContext.Rooms.Add(room);
            _dbContext.SaveChanges();
        }

        public Room GetRoom(string roomNumber)
        {
            return _dbContext.Rooms.Find(roomNumber);
        }

        public List<Room> GetAllRooms()
        {
            return _dbContext.Rooms.ToList();
        }

        public List<Room> GetRooms()
        {
            return _dbContext.Rooms.ToList();
        }

        public string UpdateRoom(string roomNumber, Room updatedRoom)
        {
            var room = _dbContext.Rooms.Find(roomNumber);
            if (room == null)
            {
                return $"Uppdatering misslyckades: Rumnummer {roomNumber} kan inte hittas.";
            }

            room.RoomSize = updatedRoom.RoomSize;
            room.Bed = updatedRoom.Bed;
            room.ExtraBed = updatedRoom.ExtraBed;

            try
            {
                _dbContext.SaveChanges();
                return $"Rumnummer {roomNumber} uppdaterades framgångsrikt.";
            }
            catch (Exception ex)
            {
                return $"Ett fel uppstod vid uppdateringen: {ex.Message}";
            }
        }

        public string DeleteRoom(Room existingRoom)
        {
            var room = _dbContext.Rooms.Find(existingRoom.RoomNumber);
            if (room == null)
            {
                return $"Radering misslyckades: Rumnummer {existingRoom.RoomNumber} kan inte hittas.";
            }

            try
            {
                _dbContext.Rooms.Remove(room);
                _dbContext.SaveChanges();
                return $"Rumnummer {existingRoom.RoomNumber} raderades framgångsrikt.";
            }
            catch (Exception ex)
            {
                return $"Ett fel uppstod vid borttagningen: {ex.Message}";
            }
        }
    }
}