using Reservroom.Models;
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
        public readonly Hotel _hotel;
        private readonly ReservationListViewModel _viewModel;

        public LoadReservationCommand(Hotel hotel, ReservationListViewModel viewModel)
        {
            _hotel = hotel;
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                IEnumerable<Reservation> reservations = await _hotel.GetAllReservations();
                _viewModel.UpdateReservation(reservations);
            }
            catch (Exception)
            {
                MessageBox.Show("Fail to load reservation.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
