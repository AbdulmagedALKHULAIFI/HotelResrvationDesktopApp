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
        //ToDo: I stopped in the 3rd video, 9th minute 
        private readonly ObservableCollection<ReservationViewModel> _reservations;
        public ICommand MakeReservationCommand { get;  }

        public ReservationListingViewModel()
        {

        }
    }
}
