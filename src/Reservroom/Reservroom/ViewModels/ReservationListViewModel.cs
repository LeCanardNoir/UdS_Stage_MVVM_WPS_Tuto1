using Reservroom.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Reservroom.ViewModels
{
    public class ReservationListViewModel : ViewModelsBase
    {
        private readonly ObservableCollection<ReservationViewModel> _reservations;
        public IEnumerable<ReservationViewModel> Reservations => _reservations;

        public ICommand MakeReservationCommand { get; }

        public ReservationListViewModel()
        {
            _reservations = new ObservableCollection<ReservationViewModel>();

            _reservations.Add(
                new ReservationViewModel(
                    new Reservation(
                        new RoomID(1,2),
                        DateTime.Now,
                        new DateTime(year:2024, month:02, day:01),
                        "burn@"
                        )
                    )
                );
            _reservations.Add(
                new ReservationViewModel(
                    new Reservation(
                        new RoomID(3, 2),
                        DateTime.Now,
                        new DateTime(year: 2024, month: 02, day: 01),
                        "God"
                        )
                    )
                );
            _reservations.Add(
                new ReservationViewModel(
                    new Reservation(
                        new RoomID(3, 2),
                        DateTime.Now,
                        new DateTime(year: 2024, month: 02, day: 01),
                        "Dumb ass"
                        )
                    )
                );
        }

    }

}
