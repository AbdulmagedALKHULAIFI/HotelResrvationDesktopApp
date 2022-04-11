using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelResrvationDesktopApp.Models
{
    public class Room
    {
        public int FloorNumber { get; set; }
        public int RoomNumber { get; set; }

        public Room(int floorNumber, int roomNumber)
        {
            FloorNumber = floorNumber;
            RoomNumber = roomNumber;
        }

        public override bool Equals(object obj)
        {
            return obj is Room room &&
                FloorNumber == room.FloorNumber &&
                RoomNumber == room.RoomNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FloorNumber, RoomNumber);
        }

        public override string ToString()
        {
            return $"{FloorNumber}{RoomNumber}";
        }

        public static bool operator ==(Room roomID1, Room roomID2)
        {
            if (roomID1 is null && roomID2 is null)
            {
                return true;
            }

            return !(roomID1 is null) && roomID1.Equals(roomID2);
        }

        public static bool operator !=(Room roomID1, Room roomID2)
        {
            return !(roomID1 == roomID2);
        }
    }
}
