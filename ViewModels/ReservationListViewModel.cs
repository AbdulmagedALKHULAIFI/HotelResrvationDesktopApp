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
    public class ReservationListViewModel : ViewModelBase
    {
        
        private readonly ObservableCollection<ReservationViewModel> _reservations;
        private HotelStore _hotelStore;

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set 
            { 
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }


        public IEnumerable<ReservationViewModel> Reservations => _reservations;

        public ICommand LoadReservationsCommand { get; }
        public ICommand MakeReservationCommand { get;  }

        public ReservationListViewModel(HotelStore hotelStore, NavigationService MakeReservationViewService)
        {
            _hotelStore = hotelStore;
            _reservations = new ObservableCollection<ReservationViewModel>();


            LoadReservationsCommand = new LoadReservationsCommand(this, _hotelStore);
            MakeReservationCommand = new NavigateCommand(MakeReservationViewService);

            _hotelStore.reservationMade += OnReservationMode;
        }

        public override void Dispose()
        {
            _hotelStore.reservationMade -= OnReservationMode;
            base.Dispose();
        }

        private void OnReservationMode(Reservation reservation)
        {
            ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
            _reservations.Add(reservationViewModel);
        }

        public static ReservationListViewModel LoadViewModel(HotelStore hotelStore,
            MakeReservationViewModel makeReservationViewModel,
            NavigationService MakeReservationViewService)
        {
            ReservationListViewModel viewModel = new ReservationListViewModel(hotelStore, MakeReservationViewService);

            viewModel.LoadReservationsCommand.Execute(null);

            return viewModel;
        }

        public void UpdateReservations(IEnumerable<Reservation> reservations)
        {
            _reservations.Clear();

            foreach (var reservation in reservations)
            {
                ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
                _reservations.Add(reservationViewModel);
            }
        }
    }
}
