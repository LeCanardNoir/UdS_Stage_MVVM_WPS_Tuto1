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
        public async Task<Reservation?> GetConflictReservation(Reservation reservation)
        {

            using (ReservroomDbContext context = _dbContextFactory.CreateDbContext())
            {
                ReservationDTO? dto = await context.Reservations
                    .Where( r => r.FloorNumber == reservation.RoomID.FloorNumber)
                    .Where(r => r.RoomNumber == reservation.RoomID.RoomNumber)
                    .Where(r => r.EndDate > reservation.StartDate)
                    .Where(r => r.StartDate < reservation.EndDate)
                    .FirstOrDefaultAsync();

                return ToReservation(dto);
            }
        }

        private static Reservation? ToReservation(ReservationDTO? dto)
        {
            if(dto == null)
                return null;
            return new Reservation(new RoomID(dto.FloorNumber, dto.RoomNumber), dto.StartDate, dto.EndDate, dto.Username);
        }
    }
}
