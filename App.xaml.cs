using HotelResrvationDesktopApp.DbContexts;
using HotelResrvationDesktopApp.Exceptions;
using HotelResrvationDesktopApp.Models;
using HotelResrvationDesktopApp.Services;
using HotelResrvationDesktopApp.Services.ReservationConflictValidators;
using HotelResrvationDesktopApp.Services.ReservationCreators;
using HotelResrvationDesktopApp.Services.ReservationProviders;
using HotelResrvationDesktopApp.Stores;
using HotelResrvationDesktopApp.ViewModels;
using Microsoft.EntityFrameworkCore;
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
        private const string ConnectionString = "Data Source=reservoom.db";
        private readonly Hotel _hotel;
        private readonly HotelStore _hotelStore;
        private readonly NavigationStore _navigationStore;
        private IReservationProvider reservationProvider;
        private IReservationCreator reservationCreator;
        private IReservationConflictValidator reservationConflictValidator;
        ReservoomDbContexFactory reservoomDbContexFactory;
      
        public App()
        {
            reservoomDbContexFactory = new ReservoomDbContexFactory(ConnectionString);

            reservationProvider = new ReservationProvider(reservoomDbContexFactory);
            reservationCreator = new ReservationCreator(reservoomDbContexFactory);
            reservationConflictValidator = new ReservationReservationValidator(reservoomDbContexFactory);

            ReservationBook reservationBook = new ReservationBook(reservationProvider, reservationCreator, reservationConflictValidator);
            _hotel = new Hotel("Ibis", reservationBook);
            _hotelStore = new HotelStore(_hotel);
            _navigationStore = new NavigationStore();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            using (ReservoomDbContext dbContext = reservoomDbContexFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            _navigationStore.CurrentviewModel = CreateReservationViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };

            MainWindow.Show();
            base.OnStartup(e);  
        }

        private MakeReservationViewModel CreateMakeReservationViewModel()
        {
            return new MakeReservationViewModel(_hotelStore, new NavigationService(_navigationStore, CreateReservationViewModel));
        }

        private ReservationListViewModel CreateReservationViewModel()
        {
            return ReservationListViewModel.LoadViewModel(_hotelStore, CreateMakeReservationViewModel() ,new NavigationService(_navigationStore, CreateMakeReservationViewModel));
        }

        //ToDo: stopped:  finished video 8
    }
}
