using HotelResrvationDesktopApp.Models;
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

        public IEnumerable<ReservationViewModel> Reservations => _reservations;
        public ICommand MakeReservationCommand { get;  }

        public ReservationListingViewModel(NavigationStore navigationStore, Func<MakeReservationViewModel> createMakeReservationViewModel)
        {
            _reservations = new ObservableCollection<ReservationViewModel>();

            MakeReservationCommand = new NavigateCommand(navigationStore,createMakeReservationViewModel);

            _reservations.Add(new ReservationViewModel(new Reservation(new Room(1, 2), "Majid", DateTime.UtcNow, DateTime.UtcNow)));
            _reservations.Add(new ReservationViewModel(new Reservation(new Room(2, 3), "Kévin", DateTime.UtcNow, DateTime.UtcNow)));
            _reservations.Add(new ReservationViewModel(new Reservation(new Room(5, 4), "Rafa", DateTime.UtcNow, DateTime.UtcNow)));
        }
    }
}
