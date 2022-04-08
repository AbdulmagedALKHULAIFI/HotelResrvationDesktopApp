using HotelResrvationDesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelResrvationDesktopApp.ViewModels
{
    public class ReservationViewModel : ViewModelBase
    {
        private Reservation _reservation;

        public string Room => _reservation.Room.ToString();
        public string Username => _reservation.Username;
        public DateTime StartTime => _reservation.StartTime;
        public DateTime EndTime => _reservation.EndTime;

        public ReservationViewModel(Reservation reservation)
        {
            _reservation = reservation;
        }
    }
}
