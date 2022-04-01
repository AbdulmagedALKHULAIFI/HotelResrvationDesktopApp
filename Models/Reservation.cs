using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelResrvationDesktopApp.Models
{
    public class Reservation
    {
        public Room Room { get; }
        public string Username { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }

        public TimeSpan Length => EndTime.Subtract(StartTime);

        public Reservation(Room room, string username, DateTime startTime, DateTime endTime)
        {
            Room = room;
            Username = username;
            StartTime = startTime;
            EndTime = endTime;
        }

        public bool Conflicts(Reservation reservation)
        {
            if (reservation.Room.Equals(Room))
                return false;
            return reservation.StartTime < EndTime && reservation.EndTime > StartTime;
        }
    }
}
