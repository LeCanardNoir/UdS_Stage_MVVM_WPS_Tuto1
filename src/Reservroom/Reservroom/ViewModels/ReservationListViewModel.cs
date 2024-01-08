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
        private readonly HotelStore _hotelStore;
        private readonly ObservableCollection<ReservationViewModel> _reservations;
        public IEnumerable<ReservationViewModel> Reservations => _reservations;

        private bool _isLoading;
        public bool IsLoading 
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(_errorMessage);

        public ICommand LoadReservationCommand { get; }
        public ICommand MakeReservationCommand { get; }

        public ReservationListViewModel(HotelStore hotelStore, NavigationServices<MakeReservationViewModel> makeReservationViewNavigationServices)
        {
            _hotelStore = hotelStore;

            _reservations = new ObservableCollection<ReservationViewModel>();
            LoadReservationCommand = new LoadReservationCommand(_hotelStore, this);
            MakeReservationCommand = new NavigateCommand<MakeReservationViewModel>(makeReservationViewNavigationServices);

            _hotelStore.ReservationMade += OnReservationMade;

            //UpdateReservation();
        }


        public override void Dispose()
        {
            _hotelStore.ReservationMade -= OnReservationMade;
            base.Dispose();
        }


        private void OnReservationMade(Reservation reservation)
        {
            ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
            _reservations.Add(reservationViewModel);
        }

        public static ReservationListViewModel LoadViewModel(HotelStore hotelStore, NavigationServices<MakeReservationViewModel> makeReservationViewNavigationServices)
        {
            ReservationListViewModel viewModel = new ReservationListViewModel(hotelStore, makeReservationViewNavigationServices);
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
