using Microsoft.EntityFrameworkCore;
using Reservroom.DbContexts;
using Reservroom.DTOs;
using Reservroom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservroom.Services.ReservationConflictValidators
{
    public class DataBaseReservationConflictValidator : IResvationConflictValidator
    {
        private readonly ReservroomDbContextFactory _dbContextFactory;

        public DataBaseReservationConflictValidator(ReservroomDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<Reservation> GetConflictReservation(Reservation reservation)
        {

            using (ReservroomDbContext context = _dbContextFactory.CreateDbContext())
            {
                ReservationDTO dto = new ReservationDTO()
                {
                    FloorNumber = reservation.RoomID?.FloorNumber ?? 0,
                    RoomNumber = reservation.RoomID?.RoomNumber ?? 0,
                    Username = reservation.Username,
                    StartDate = reservation.StartDate,
                    EndDate = reservation.EndDate
                };
                //TODO: Pas trop efficace
                return await context.Reservations.Select( r => ToReservation(r)).FirstOrDefaultAsync(r=>r.Conflict(reservation));
            }
        }

        private static Reservation ToReservation(ReservationDTO dto)
        {
            return new Reservation(new RoomID(dto.FloorNumber, dto.RoomNumber), dto.StartDate, dto.EndDate, dto.Username);
        }
    }
}
