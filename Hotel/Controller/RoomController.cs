using Hotel.Data;
using Hotel.Menus;
using Hotel.Entities;
using Hotel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Controller
{
    public class RoomController
    {
        private RoomService _roomService;

        public RoomController(RoomService roomService)
        {
            _roomService = roomService;
        }
        public void CreateRoom()
        {
            throw new NotImplementedException();
        }

        public void UpdateRoom()
        {
            throw new NotImplementedException();
        }

        public void DeleteRoom()
        {
            throw new NotImplementedException();
        }

        public void ListRooms()
        {
            throw new NotImplementedException();
        }
    }
}
