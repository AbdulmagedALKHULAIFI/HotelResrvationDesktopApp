using HotelResrvationDesktopApp.Models;
using HotelResrvationDesktopApp.Stores;
using HotelResrvationDesktopApp.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelResrvationDesktopApp.ViewModels
{
    public class LoadReservationsCommand : AsyncCommandbase
    {
       
        private readonly ReservationListViewModel _viewModel;
        private readonly HotelStore _hotelStore;

        public LoadReservationsCommand(ReservationListViewModel viewModel, HotelStore hotelStore)
        {
            _hotelStore = hotelStore;
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _hotelStore.Load();
                _viewModel.UpdateReservations(_hotelStore.Reservations);
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load the reservations.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
