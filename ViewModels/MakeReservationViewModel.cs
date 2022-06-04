using HotelResrvationDesktopApp.Models;
using HotelResrvationDesktopApp.Services;
using HotelResrvationDesktopApp.Stores;
using HotelResrvationDesktopApp.ViewModels.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelResrvationDesktopApp.ViewModels
{
    public class MakeReservationViewModel: ViewModelBase, INotifyDataErrorInfo
    {
        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
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


        private DateTime _startDate = DateTime.UtcNow;
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));

                ClearErrors(nameof(StartDate));
                ClearErrors(nameof(EndDate));

                if (EndDate < StartDate)
                {
                    AddError(nameof(StartDate), "The start date cannot be before the end date.");
                }
            }
        }


        private DateTime _endDate = DateTime.UtcNow;

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
                    AddError(nameof(EndDate), "The end date cannot be before the start date.");
                }
            }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDicitionary;

        public bool HasErrors => _propertyNameToErrorsDicitionary.Any();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public MakeReservationViewModel(HotelStore hotelStore, NavigationService reservationViewNavigationService)
        {
            SubmitCommand = new MakeReservationCommand(this, hotelStore, reservationViewNavigationService);
            CancelCommand = new NavigateCommand(reservationViewNavigationService);
            _propertyNameToErrorsDicitionary = new Dictionary<string, List<string>>();
        }


        private void AddError(string propertyName, string errorMessage)
        {
            if (!_propertyNameToErrorsDicitionary.ContainsKey(propertyName))
            {
                _propertyNameToErrorsDicitionary.Add(propertyName, new List<string>());
            }

            _propertyNameToErrorsDicitionary[propertyName].Add(errorMessage);

            OnErrorsChanged(propertyName);
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return _propertyNameToErrorsDicitionary.GetValueOrDefault(propertyName, new List<string>());
        }
        private void ClearErrors(string propertyName)
        {
            _propertyNameToErrorsDicitionary.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
