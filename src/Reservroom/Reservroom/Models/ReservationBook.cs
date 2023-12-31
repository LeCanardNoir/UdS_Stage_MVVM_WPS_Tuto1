using Reservroom.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservroom.Models
{
    public class ReservationBook
    {
        private readonly List<Reservation> _reservations;

        public ReservationBook()
        {
            _reservations = new List<Reservation>();
        }

        public IEnumerable<Reservation> GetReservationsForUser(string username)
        {
            return _reservations.Where(x => x.Username == username);
        }

        /// <summary>
        /// Get all reservations
        /// </summary>
        /// <returns>Reservations list</returns>
        public IEnumerable<Reservation> GetAllReservations()
        {
            return _reservations;
        }

        public void AddReservation(Reservation reservation)
        {
            if (reservation == null) throw new ArgumentNullException(nameof(reservation));

            if (_reservations.Find(x => x.Conflict(reservation)) is Reservation existingReservation)
                throw new ReservationConflictException(existingReservation, reservation);

            _reservations.Add(reservation);
        }

    }
}
