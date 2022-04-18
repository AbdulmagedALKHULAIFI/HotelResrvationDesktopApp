using HotelResrvationDesktopApp.Models;
using HotelResrvationDesktopApp.Services;
using HotelResrvationDesktopApp.Stores;
using HotelResrvationDesktopApp.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelResrvationDesktopApp.ViewModels
{
    class ReservationListingViewModel : ViewModelBase
    {
        
        private readonly ObservableCollection<ReservationViewModel> _reservations;
        private readonly Hotel _hotel;

        public IEnumerable<ReservationViewModel> Reservations => _reservations;
        public ICommand MakeReservationCommand { get;  }

        public ReservationListingViewModel(Hotel hotel, NavigationService MakeReservationViewService)
        {
            _reservations = new ObservableCollection<ReservationViewModel>();
            _hotel = hotel;
            MakeReservationCommand = new NavigateCommand(MakeReservationViewService);

            UpdateReservations();
        }

        private void UpdateReservations()
        {
            _reservations.Clear();

            foreach (var reservation in _hotel.GetallReservations())
            {
                ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
                _reservations.Add(reservationViewModel);
            }
        }
    }
}
