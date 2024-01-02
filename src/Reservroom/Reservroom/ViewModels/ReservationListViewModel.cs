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

        public ICommand LoadReservationCommand { get; }
        public ICommand MakeReservationCommand { get; }

        public ReservationListViewModel(Hotel hotel, NavigationServices makeReservationViewNavigationServices)
        {
            _hotel = hotel;

            _reservations = new ObservableCollection<ReservationViewModel>();
            LoadReservationCommand = new LoadReservationCommand(_hotel, this);
            MakeReservationCommand = new NavigateCommand(makeReservationViewNavigationServices);

            //UpdateReservation();
        }

        public static ReservationListViewModel LoadViewModel(Hotel hotel, NavigationServices makeReservationViewNavigationServices)
        {
            ReservationListViewModel viewModel = new ReservationListViewModel(hotel, makeReservationViewNavigationServices);
            viewModel.LoadReservationCommand.Execute(null);
            return viewModel;
        }

        public void UpdateReservation(IEnumerable<Reservation> reservations)
        {
            _reservations.Clear();
            foreach (Reservation item in reservations)
            {
                ReservationViewModel reservationViewModel = new ReservationViewModel(item);
                _reservations.Add(reservationViewModel);
            }

        }
    }

}
