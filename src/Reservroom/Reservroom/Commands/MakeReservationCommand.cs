﻿using Reservroom.Exceptions;
using Reservroom.Models;
using Reservroom.Services;
using Reservroom.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Reservroom.Commands
{
    public class MakeReservationCommand : CommandBase
    {
        private readonly MakeReservationViewModel _makeReservationViewModel;
        private readonly Hotel _hotel;
        private readonly NavigationServices _reservationViewNavigationServices;

        public MakeReservationCommand(ViewModels.MakeReservationViewModel makeReservationViewModel, Hotel hotel, NavigationServices reservationViewNavigationServices)
        {
            this._makeReservationViewModel = makeReservationViewModel;
            _hotel = hotel;
            _reservationViewNavigationServices = reservationViewNavigationServices;
            _makeReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if( e.PropertyName == nameof(MakeReservationViewModel.Username) ||
                e.PropertyName == nameof(MakeReservationViewModel.FloorNumber) ||
                e.PropertyName == nameof(MakeReservationViewModel.RoomNumber)
            )
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_makeReservationViewModel.Username) && 
                _makeReservationViewModel.FloorNumber > 0 &&
                _makeReservationViewModel.RoomNumber > 0 &&
                base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            Reservation reservation = new Reservation(
                new RoomID(_makeReservationViewModel.FloorNumber, _makeReservationViewModel.RoomNumber),
                _makeReservationViewModel.StatDate,
                _makeReservationViewModel.EndDate,
                _makeReservationViewModel.Username
                );

            try
            {
                _hotel.MakeReservation(reservation);
                MessageBox.Show("Reservation succeed", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _reservationViewNavigationServices.Navigate();
            }
            catch (ReservationConflictException ex) 
            {
                MessageBox.Show("This reservation allready existed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}