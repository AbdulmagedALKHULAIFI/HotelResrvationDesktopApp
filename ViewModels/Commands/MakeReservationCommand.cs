using HotelResrvationDesktopApp.Exceptions;
using HotelResrvationDesktopApp.Models;
using HotelResrvationDesktopApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelResrvationDesktopApp.ViewModels.Commands
{
    class MakeReservationCommand : AsyncCommandbase
    {
        private readonly Hotel _hotel;
        private readonly NavigationService _reservationViewNavigationService;
        private readonly MakeReservationViewModel _makeReservationViewModel;
        public MakeReservationCommand(MakeReservationViewModel makeReservationViewModel, 
            Hotel hotel,
            NavigationService reservationViewNavigationService)
        {
            _hotel = hotel;
            _reservationViewNavigationService = reservationViewNavigationService;
            _makeReservationViewModel = makeReservationViewModel;

            //ToDo: read about this part "Subscription"
            _makeReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_makeReservationViewModel.Username) &&
                _makeReservationViewModel.FloorNumber > 0 && 
                base.CanExecute(parameter);
        }
        public override async Task ExecuteAsync(object parameter)
        {
            Reservation reservation = new(
                new Room(_makeReservationViewModel.FloorNumber, _makeReservationViewModel.RoomNumber),
                _makeReservationViewModel.Username,
                _makeReservationViewModel.StartDate,
                _makeReservationViewModel.EndDate);

            try
            {
                await _hotel.MakeReservation(reservation);
                MessageBox.Show("Successfully reserved room", "success", MessageBoxButton.OK, MessageBoxImage.Information);
                _reservationViewNavigationService.Navigate();
            }
            catch (ReservationConflictException)
            {
                MessageBox.Show("This room is already taken", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Failde to make a reservation", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(MakeReservationViewModel.Username) ||
                e.PropertyName == nameof(MakeReservationViewModel.FloorNumber))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
