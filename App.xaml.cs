using HotelResrvationDesktopApp.DbContexts;
using HotelResrvationDesktopApp.Exceptions;
using HotelResrvationDesktopApp.Models;
using HotelResrvationDesktopApp.Services;
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
        private readonly NavigationStore _navigationStore;
        public App()
        {
            _hotel = new Hotel("Ibis");
            _navigationStore = new NavigationStore();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(ConnectionString).Options;

            using (ReservoomDbContext dbContext = new ReservoomDbContext(options))
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
            return new MakeReservationViewModel(_hotel, new NavigationService(_navigationStore, CreateReservationViewModel));
        }

        private ReservationListingViewModel CreateReservationViewModel()
        {
            return new ReservationListingViewModel(_hotel, new NavigationService(_navigationStore, CreateMakeReservationViewModel));
        }

        //ToDo: stopped video 6 before beginning of "updating the app.xaml.cs setup 
    }
}
