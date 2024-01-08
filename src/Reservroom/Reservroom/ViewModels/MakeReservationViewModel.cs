using Reservroom.Commands;
using Reservroom.Models;
using Reservroom.Services;
using Reservroom.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Reservroom.ViewModels
{
    public class MakeReservationViewModel : ViewModelBase, INotifyDataErrorInfo
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
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));

                ClearErrors(nameof(StartDate));
                ClearErrors(nameof(EndDate));

                if (StartDate > EndDate)
                {
                    AddErrors("The start date cannot be after the end date.", nameof(StartDate));
                }
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

                ClearErrors(nameof(StartDate));
                ClearErrors(nameof(EndDate));

                if (EndDate < StartDate)
                {
                    //AddErrors("The start date cannot be after the end date.", nameof(StartDate));
                    AddErrors("The end date cannot be before the start date.", nameof(EndDate));
                }

            }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionnary;
        public bool HasErrors => _propertyNameToErrorsDictionnary.Any();
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public MakeReservationViewModel(HotelStore hotelStore, NavigationServices<ReservationListViewModel> reservationViewNavigationServices)
        {
            SubmitCommand = new MakeReservationCommand(this, hotelStore, reservationViewNavigationServices);
            CancelCommand = new NavigateCommand<ReservationListViewModel>(reservationViewNavigationServices);
            _propertyNameToErrorsDictionnary = new Dictionary<string, List<string>>();
        }

        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyNameToErrorsDictionnary.GetValueOrDefault(propertyName, new List<string>());
        }

        private void ClearErrors(string propertyName)
        {
            _propertyNameToErrorsDictionnary.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }

        private void AddErrors(string errorMessage, string propertyName)
        {
            if(!_propertyNameToErrorsDictionnary.ContainsKey(propertyName))
            {
                _propertyNameToErrorsDictionnary.Add(propertyName, new List<string>());
            }

            _propertyNameToErrorsDictionnary[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);

        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
