using Reservroom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservroom.ViewModels
{
    public class ReservationViewModel : ViewModelBase
    {
        private Reservation _reservation;

        public string RoomID => _reservation.RoomID.ToString();
        public string Username => _reservation.Username;
        public DateTime StartDate => _reservation.StartDate;
        public DateTime EndDate => _reservation.EndDate;

        public ReservationViewModel(Reservation reservation)
        {
            _reservation = reservation;
        }
    }
  
}
