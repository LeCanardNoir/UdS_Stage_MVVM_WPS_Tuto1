using Reservroom.Models;
using Reservroom.Stores;
using Reservroom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Reservroom.Commands
{
    public class LoadReservationCommand : AsyncCommandBase
    {
        public readonly HotelStore _hotelStore;
        private readonly ReservationListViewModel _viewModel;

        public LoadReservationCommand(HotelStore hotelStore, ReservationListViewModel viewModel)
        {
            _hotelStore = hotelStore;
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                await _hotelStore.Load();
                _viewModel.UpdateReservation(_hotelStore.Reservations);
            }
            catch (Exception)
            {
                MessageBox.Show("Fail to load reservation.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
