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
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public string Username { get; }
        public TimeSpan Length => EndDate.Subtract(StartDate);

        public Reservation(RoomID roomID, DateTime startTime, DateTime endTime, string username)
        {
            RoomID = roomID;
            StartDate = startTime;
            EndDate = endTime;
            Username = username;
        }

        internal bool Conflict(Reservation reservation)
        {
            if (reservation == null)
                throw new ArgumentNullException();

            if(reservation.RoomID != RoomID)
                return false;

            return reservation.StartDate < EndDate && reservation.EndDate > StartDate;
        }
    }
}
