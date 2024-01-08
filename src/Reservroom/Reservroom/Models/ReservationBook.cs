using Reservroom.Exceptions;
using Reservroom.Services.ReservationConflictValidators;
using Reservroom.Services.ReservationCreators;
using Reservroom.Services.ReservationProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservroom.Models
{
    public class ReservationBook
    {
        private readonly IReservationProvider _reservationProvider;
        private readonly IReservationCreator _reservationCreator;
        private readonly IResvationConflictValidator _reservationConflictValidator;


        public ReservationBook(IReservationProvider reservationProvider, IReservationCreator reservationCreator, IResvationConflictValidator reservationConflictValidator)
        {
            _reservationProvider = reservationProvider;
            _reservationCreator = reservationCreator;
            _reservationConflictValidator = reservationConflictValidator;
        }

        //public IEnumerable<Reservation> GetReservationsForUser(string username)
        //{
        //    return _reservationProvider.GetAllReservation().Where(x => x.Username == username);
        //}

        /// <summary>
        /// Get all reservations
        /// </summary>
        /// <returns>Reservations list</returns>
        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
        {
            return await _reservationProvider.GetAllReservation();
        }

        public async Task AddReservationAsync(Reservation reservation)
        {
            if (reservation == null) throw new ArgumentNullException(nameof(reservation));

            Reservation conflictReservation = await _reservationConflictValidator.GetConflictReservation(reservation);
            if (conflictReservation != null)
                throw new ReservationConflictException(conflictReservation, reservation);

            //List<Reservation> allReservations = (await _reservationProvider.GetAllReservation()).ToList();

            //if (allReservations.Find(x => x.Conflict(reservation)) is Reservation existingReservation)
            //    throw new ReservationConflictException(existingReservation, reservation);

            await _reservationCreator.CreateReservation(reservation);
        }

    }
}
