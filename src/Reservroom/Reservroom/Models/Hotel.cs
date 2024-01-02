using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservroom.Models
{
    public class Hotel
    {
        private readonly ReservationBook _reservationBook;

        public string Name {  get; }

        public Hotel(string name, ReservationBook reservationBook)
        {
            Name = name;
            _reservationBook = reservationBook;
        }

        /// <summary>
        /// Get reservation for user
        /// </summary>
        /// <param name="username">The user of the user</param>
        /// <returns>Reservations list of the user</returns>
        //public async Task<IEnumerable<Reservation>> GetReservationsForUser(string username) => await _reservationBook.GetReservationsForUser(username);


        /// <summary>
        /// Get all reservation
        /// </summary>
        /// <param name="username">The user of the user</param>
        /// <returns>Reservations list</returns>
        public async Task<IEnumerable<Reservation>> GetAllReservations() => await _reservationBook.GetAllReservationsAsync();

        /// <summary>
        /// Make a reservation
        /// </summary>
        /// <param name="reservation"></param>
        /// <exception cref="ReservationConflictException"
        public async Task MakeReservation(Reservation reservation) => await _reservationBook.AddReservationAsync(reservation);

    }
}
