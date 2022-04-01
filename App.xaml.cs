using HotelResrvationDesktopApp.Exceptions;
using HotelResrvationDesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HotelResrvationDesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Hotel hotel = new Hotel("Intercontinetal");

            try
            {
                hotel.MakeReservation(new Reservation(
                new Room(1, 2),
                "Brevent",
                new DateTime(2000, 1, 1),
                new DateTime(2000, 1, 2)
                ));

                hotel.MakeReservation(new Reservation(
                    new Room(1, 3),
                    "Brevent",
                    new DateTime(2000, 1, 8),
                    new DateTime(2000, 1, 10)
                    ));

                IEnumerable<Reservation> reservations = hotel.GetReservationsForUser("Brevent");
            }
            catch (ReservationConflictException ex)
            {

            }
           
            base.OnStartup(e);  
        }
       
    }
}
