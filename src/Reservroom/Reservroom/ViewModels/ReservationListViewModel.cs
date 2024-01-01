using Reservroom.Commands;
using Reservroom.Models;
using Reservroom.Services;
using Reservroom.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Reservroom.ViewModels
{
    public class ReservationListViewModel : ViewModelBase
    {
        private readonly Hotel _hotel;
        private readonly ObservableCollection<ReservationViewModel> _reservations;
        public IEnumerable<ReservationViewModel> Reservations => _reservations;

        public ICommand MakeReservationCommand { get; }

        public ReservationListViewModel(Hotel hotel, NavigationServices makeReservationViewNavigationServices)
        {
            _hotel = hotel;

            _reservations = new ObservableCollection<ReservationViewModel>();

            MakeReservationCommand = new NavigateCommand(makeReservationViewNavigationServices);

            UpdateReservation();
        }

        private void UpdateReservation()
        {
            _reservations.Clear();
            foreach (Reservation item in _hotel.GetAllReservations())
            {
                ReservationViewModel reservationViewModel = new ReservationViewModel(item);
                _reservations.Add(reservationViewModel);
            }

        }
    }

}
