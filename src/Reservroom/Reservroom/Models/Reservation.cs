using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservroom.Models
{
    public class Reservation
    {

        public RoomID RoomID { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public string Username { get; }
        public TimeSpan Length => EndTime.Subtract(StartTime);

        public Reservation(RoomID roomID, DateTime startTime, DateTime endTime, string username)
        {
            RoomID = roomID;
            StartTime = startTime;
            EndTime = endTime;
            Username = username;
        }

        internal bool Conflict(Reservation reservation)
        {
            if (reservation == null)
                throw new ArgumentNullException();

            if(reservation.RoomID != RoomID)
                return false;

            return reservation.StartTime < EndTime && reservation.EndTime > StartTime;
        }
    }
}
