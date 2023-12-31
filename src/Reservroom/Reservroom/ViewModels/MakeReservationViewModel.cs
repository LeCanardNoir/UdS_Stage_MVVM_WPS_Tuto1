﻿using Reservroom.Commands;
using Reservroom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Reservroom.ViewModels
{
    public class MakeReservationViewModel : ViewModelsBase
    {
        private string _username;
        public string Username 
        { 
            get { return _username;}
            set 
            { 
                _username = value; 
                OnPropertyChanged(nameof(Username));
            }
        }

        private int _floorNumber;
        public int FloorNumber
        {
            get { return _floorNumber; }
            set
            {
                _floorNumber = value;
                OnPropertyChanged(nameof(FloorNumber));
            }
        }

        private int _roomNumber;
        public int RoomNumber
        {
            get { return _roomNumber; }
            set
            {
                _roomNumber = value;
                OnPropertyChanged(nameof(RoomNumber));
            }
        }

        private DateTime _startDate = DateTime.Now;
        public DateTime StatDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StatDate));
            }
        }

        private DateTime _endDate = DateTime.Now.AddDays(1);
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public MakeReservationViewModel(Hotel hotel)
        {
            SubmitCommand = new MakeReservationCommand(this, hotel);
            CancelCommand = new CancelMakeReservationCommand();
        }

    }
}
