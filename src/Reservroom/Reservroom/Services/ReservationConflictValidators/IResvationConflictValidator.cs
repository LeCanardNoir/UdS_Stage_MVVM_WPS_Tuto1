using Reservroom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservroom.Services.ReservationConflictValidators
{
    public interface IResvationConflictValidator
    {
        Task<Reservation> GetConflictReservation(Reservation reservation);
    }
}
